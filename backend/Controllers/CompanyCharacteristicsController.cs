using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyCharacteristicsController(FitBackendContext context, IMapper mapper)
  : ControllerBase
{
  private readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpPost]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> PostCompanyCharacteristic(
    [FromBody] CompanyCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    if (
      !CompanyExists(companyCharacteristicCreateDto.CompanyId)
      || !CharacteristicExists(companyCharacteristicCreateDto.CharacteristicId)
    )
    {
      return BadRequest();
    }

    if (
      await _context.CompanyCharacteristics.AnyAsync(companyCharacteristic =>
        companyCharacteristic.CompanyId == companyCharacteristicCreateDto.CompanyId
        && companyCharacteristic.CharacteristicId == companyCharacteristicCreateDto.CharacteristicId
      )
    )
    {
      return BadRequest();
    }

    var companyCharacteristic = _mapper.Map<CompanyCharacteristic>(companyCharacteristicCreateDto);
    _context.CompanyCharacteristics.Add(companyCharacteristic);
    await _context.SaveChangesAsync();

    return CreatedAtAction(null, new { id = companyCharacteristic.Id });
  }

  [HttpPut("{id}")]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> PutCompanyCharacteristic(
    [FromRoute] Guid id,
    [FromBody] CompanyCharacteristicUpdateDto companyCharacteristicUpdateDto
  )
  {
    var companyCharacteristic = await TryGetCompanyCharacteristicAsync(id);
    if (companyCharacteristic == null)
    {
      return NotFound();
    }

    _mapper.Map(companyCharacteristicUpdateDto, companyCharacteristic);
    _context.Entry(companyCharacteristic).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!CompanyCharacteristicExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteCompanyCharacteristic([FromRoute] Guid id)
  {
    var companyCharacteristic = await TryGetCompanyCharacteristicAsync(id);
    if (companyCharacteristic == null)
    {
      return NotFound();
    }

    _context.CompanyCharacteristics.Remove(companyCharacteristic);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool CompanyExists(Guid id)
  {
    return _context.Companies.Any(company => company.Id == id);
  }

  private bool CharacteristicExists(Guid id)
  {
    return _context.Characteristics.Any(characteristic => characteristic.Id == id);
  }

  private bool CompanyCharacteristicExists(Guid id)
  {
    return _context.CompanyCharacteristics.Any(companyCharacteristic =>
      companyCharacteristic.Id == id
    );
  }

  private async Task<CompanyCharacteristic?> TryGetCompanyCharacteristicAsync(Guid id)
  {
    return await _context.CompanyCharacteristics.FindAsync(id);
  }
}
