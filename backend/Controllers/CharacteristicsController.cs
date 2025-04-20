using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

public abstract class CharacteristicsController<
  TCharacteristic,
  TCharacteristicReadDto,
  TCharacteristicCreateDto,
  TCharacteristicUpdateDto
>(FitBackendContext context, IMapper mapper) : ControllerBase
  where TCharacteristic : Entity
  where TCharacteristicReadDto : EntityReadDto
{
  private readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpGet]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<List<TCharacteristicReadDto>>> GetCharacteristics()
  {
    var characteristics = await AddDefaultIncludes(_context.Set<TCharacteristic>())
      .AsNoTracking()
      .ToListAsync();
    return Ok(_mapper.Map<List<TCharacteristicReadDto>>(characteristics));
  }

  [HttpGet("{id}")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<TCharacteristicReadDto>> GetCharacteristic([FromRoute] Guid id)
  {
    var characteristic = await AddDefaultIncludes(_context.Set<TCharacteristic>())
      .AsNoTracking()
      .FirstOrDefaultAsync(characteristic => characteristic.Id == id);
    if (characteristic == null)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<TCharacteristicReadDto>(characteristic));
  }

  [HttpPost]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<TCharacteristicReadDto>> PostCharacteristic(
    [FromBody] TCharacteristicCreateDto characteristicCreateDto
  )
  {
    var characteristic = _mapper.Map<TCharacteristic>(characteristicCreateDto);
    _context.Set<TCharacteristic>().Add(characteristic);
    await _context.SaveChangesAsync();

    var characteristicReadDto = _mapper.Map<TCharacteristicReadDto>(characteristic);
    return CreatedAtAction(
      nameof(GetCharacteristic),
      new { id = characteristic.Id },
      characteristicReadDto
    );
  }

  [HttpPut("{id}")]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> PutCharacteristic(
    [FromRoute] Guid id,
    [FromBody] TCharacteristicUpdateDto characteristicUpdateDto
  )
  {
    var characteristic = await _context
      .Set<TCharacteristic>()
      .FirstOrDefaultAsync(characteristic => characteristic.Id == id);
    if (characteristic == null)
    {
      return NotFound();
    }

    _mapper.Map(characteristicUpdateDto, characteristic);
    _context.Entry(characteristic).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!_context.Set<TCharacteristic>().Any(characteristic => characteristic.Id == id))
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
  public async Task<IActionResult> DeleteCharacteristic([FromRoute] Guid id)
  {
    var characteristic = await _context
      .Set<TCharacteristic>()
      .FirstOrDefaultAsync(characteristic => characteristic.Id == id);
    if (characteristic == null)
    {
      return NotFound();
    }

    _context.Set<TCharacteristic>().Remove(characteristic);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  protected abstract IQueryable<TCharacteristic> AddDefaultIncludes(
    IQueryable<TCharacteristic> characteristics
  );
}
