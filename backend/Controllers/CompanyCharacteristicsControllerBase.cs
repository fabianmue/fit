using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

public abstract class CompanyCharacteristicsControllerBase<
  TCompanyCharacteristic,
  TCharacteristic,
  TCompanyCharacteristicReadDto,
  TCompanyCharacteristicCreateDto,
  TCompanyCharacteristicUpdateDto
>(FitBackendContext context, IMapper mapper) : ControllerBase
  where TCompanyCharacteristic : Entity
  where TCharacteristic : Entity
{
  protected readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpPost]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<TCompanyCharacteristicReadDto>> PostCompanyCharacteristic(
    [FromBody] TCompanyCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    if (!await AllRelatedEntitiesExistAsync(companyCharacteristicCreateDto))
    {
      return BadRequest();
    }

    if (await CompanyCharacteristicWithSameParentsExistsAsync(companyCharacteristicCreateDto))
    {
      return BadRequest();
    }

    var companyCharacteristic = _mapper.Map<TCompanyCharacteristic>(companyCharacteristicCreateDto);
    _context.Set<TCompanyCharacteristic>().Add(companyCharacteristic);
    await _context.SaveChangesAsync();

    var companyCharacteristicReadDto = _mapper.Map<TCompanyCharacteristicReadDto>(
      companyCharacteristic
    );
    return CreatedAtAction(
      null,
      new { id = companyCharacteristic.Id },
      companyCharacteristicReadDto
    );
  }

  [HttpPut("{id}")]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<TCompanyCharacteristicReadDto>> PutCompanyCharacteristic(
    [FromRoute] Guid id,
    [FromBody] TCompanyCharacteristicUpdateDto companyCharacteristicUpdateDto
  )
  {
    var companyCharacteristic = await AddDefaultIncludes(_context.Set<TCompanyCharacteristic>())
      .FirstOrDefaultAsync(companyCharacteristic => companyCharacteristic.Id == id);
    if (companyCharacteristic == null)
    {
      return NotFound();
    }

    ClearHistoricValuesIfNeeded(companyCharacteristic);
    _mapper.Map(companyCharacteristicUpdateDto, companyCharacteristic);
    _context.Entry(companyCharacteristic).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (
        !_context
          .Set<TCompanyCharacteristic>()
          .Any(companyCharacteristic => companyCharacteristic.Id == id)
      )
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
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteCompanyCharacteristic([FromRoute] Guid id)
  {
    var companyCharacteristic = await _context
      .Set<TCompanyCharacteristic>()
      .FirstOrDefaultAsync(companyCharacteristic => companyCharacteristic.Id == id);
    if (companyCharacteristic == null)
    {
      return NotFound();
    }

    _context.Set<TCompanyCharacteristic>().Remove(companyCharacteristic);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  protected abstract Task<bool> AllRelatedEntitiesExistAsync(
    TCompanyCharacteristicCreateDto companyCharacteristicCreateDto
  );

  protected abstract Task<bool> CompanyCharacteristicWithSameParentsExistsAsync(
    TCompanyCharacteristicCreateDto companyCharacteristicCreateDto
  );

  protected abstract void ClearHistoricValuesIfNeeded(TCompanyCharacteristic companyCharacteristic);

  protected abstract IQueryable<TCompanyCharacteristic> AddDefaultIncludes(
    IQueryable<TCompanyCharacteristic> companyCharacteristics
  );
}
