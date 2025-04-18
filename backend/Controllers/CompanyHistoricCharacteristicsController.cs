using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyHistoricCharacteristicsController(FitBackendContext context, IMapper mapper)
  : ControllerBase
{
  private readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpPost]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> PostCompanyHistoricCharacteristic(
    [FromBody] CompanyHistoricCharacteristicCreateDto companyHistoricCharacteristicCreateDto
  )
  {
    if (
      !CompanyExists(companyHistoricCharacteristicCreateDto.CompanyId)
      || !HistoricCharacteristicExists(
        companyHistoricCharacteristicCreateDto.HistoricCharacteristicId
      )
    )
    {
      return BadRequest();
    }

    if (
      await _context.CompanyHistoricCharacteristics.AnyAsync(companyHistoricCharacteristic =>
        companyHistoricCharacteristic.CompanyId == companyHistoricCharacteristicCreateDto.CompanyId
        && companyHistoricCharacteristic.HistoricCharacteristicId
          == companyHistoricCharacteristicCreateDto.HistoricCharacteristicId
      )
    )
    {
      return BadRequest();
    }

    var companyHistoricCharacteristic = _mapper.Map<CompanyHistoricCharacteristic>(
      companyHistoricCharacteristicCreateDto
    );
    _context.CompanyHistoricCharacteristics.Add(companyHistoricCharacteristic);
    await _context.SaveChangesAsync();

    return CreatedAtAction(null, new { id = companyHistoricCharacteristic.Id });
  }

  [HttpPut("{id}")]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> PutCompanyHistoricCharacteristic(
    [FromRoute] Guid id,
    [FromBody] CompanyHistoricCharacteristicUpdateDto companyHistoricCharacteristicUpdateDto
  )
  {
    var companyHistoricCharacteristic = await TryGetCompanyHistoricCharacteristicAsync(id);
    if (companyHistoricCharacteristic == null)
    {
      return NotFound();
    }

    companyHistoricCharacteristic.Values.Clear();
    _mapper.Map(companyHistoricCharacteristicUpdateDto, companyHistoricCharacteristic);
    _context.Entry(companyHistoricCharacteristic).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!CompanyHistoricCharacteristicExists(id))
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
  [Authorize("Authenticated")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteCompanyHistoricCharacteristic([FromRoute] Guid id)
  {
    var companyHistoricCharacteristic = await TryGetCompanyHistoricCharacteristicAsync(id);
    if (companyHistoricCharacteristic == null)
    {
      return NotFound();
    }

    _context.CompanyHistoricCharacteristics.Remove(companyHistoricCharacteristic);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool CompanyExists(Guid id)
  {
    return _context.Companies.Any(company => company.Id == id);
  }

  private bool HistoricCharacteristicExists(Guid id)
  {
    return _context.HistoricCharacteristics.Any(historicCharacteristic =>
      historicCharacteristic.Id == id
    );
  }

  private bool CompanyHistoricCharacteristicExists(Guid id)
  {
    return _context.CompanyHistoricCharacteristics.Any(companyHistoricCharacteristic =>
      companyHistoricCharacteristic.Id == id
    );
  }

  private async Task<CompanyHistoricCharacteristic?> TryGetCompanyHistoricCharacteristicAsync(
    Guid id
  )
  {
    return await _context
      .CompanyHistoricCharacteristics.Include(companyHistoricCharacteristic =>
        companyHistoricCharacteristic.Values
      )
      .FirstOrDefaultAsync(companyHistoricCharacteristic => companyHistoricCharacteristic.Id == id);
  }
}
