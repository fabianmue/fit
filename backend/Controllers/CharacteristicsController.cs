using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CharacteristicsController(FitBackendContext context, IMapper mapper) : ControllerBase
{
  private readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpGet]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<List<CharacteristicReadDto>>> GetCharacteristics()
  {
    var characteristics = await _context
      .Characteristics.Include(characteristic => characteristic.CompanyCharacteristics)
      .AsNoTracking()
      .ToListAsync();
    return Ok(_mapper.Map<List<CharacteristicReadDto>>(characteristics));
  }

  [HttpGet("{id}")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CharacteristicReadDto>> GetCharacteristic([FromRoute] Guid id)
  {
    var characteristic = await TryGetCharacteristicAsync(id);
    if (characteristic == null)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<CharacteristicReadDto>(characteristic));
  }

  [HttpPost]
  [Authorize("Authenticated")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<CharacteristicReadDto>> PostCharacteristic(
    [FromBody] CharacteristicCreateDto characteristicCreateDto
  )
  {
    var characteristic = _mapper.Map<Characteristic>(characteristicCreateDto);
    _context.Characteristics.Add(characteristic);
    await _context.SaveChangesAsync();

    var characteristicReadDto = _mapper.Map<CharacteristicReadDto>(characteristic);
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
    [FromBody] CharacteristicUpdateDto characteristicUpdateDto
  )
  {
    var characteristic = await TryGetCharacteristicAsync(id);
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
      if (!CharacteristicExists(id))
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
    var characteristic = await TryGetCharacteristicAsync(id);
    if (characteristic == null)
    {
      return NotFound();
    }

    _context.Characteristics.Remove(characteristic);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool CharacteristicExists(Guid id)
  {
    return _context.Characteristics.Any(characteristic => characteristic.Id == id);
  }

  private async Task<Characteristic?> TryGetCharacteristicAsync(Guid id)
  {
    return await _context.Characteristics.FindAsync(id);
  }
}
