using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class HistoricCharacteristicsController(FitBackendContext context, IMapper mapper)
  : ControllerBase
{
  private readonly FitBackendContext _context = context;

  private readonly IMapper _mapper = mapper;

  [HttpGet]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<List<HistoricCharacteristicReadDto>>> GetHistoricCharacteristics()
  {
    var historicCharacteristics = await _context
      .HistoricCharacteristics.Include(historicCharacteristic =>
        historicCharacteristic.CompanyHistoricCharacteristics
      )
      .AsNoTracking()
      .ToListAsync();
    return Ok(_mapper.Map<List<HistoricCharacteristicReadDto>>(historicCharacteristics));
  }

  [HttpGet("{id}")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<HistoricCharacteristicReadDto>> GetHistoricCharacteristic(
    [FromRoute] Guid id
  )
  {
    var historicCharacteristic = await TryGetHistoricCharacteristicAsync(id);
    if (historicCharacteristic == null)
    {
      return NotFound();
    }

    return Ok(_mapper.Map<HistoricCharacteristicReadDto>(historicCharacteristic));
  }

  [HttpPost]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<HistoricCharacteristicReadDto>> PostHistoricCharacteristic(
    [FromBody] HistoricCharacteristicCreateDto historicCharacteristicCreateDto
  )
  {
    var historicCharacteristic = _mapper.Map<HistoricCharacteristic>(
      historicCharacteristicCreateDto
    );
    _context.HistoricCharacteristics.Add(historicCharacteristic);
    await _context.SaveChangesAsync();

    var historicCharacteristicReadDto = _mapper.Map<HistoricCharacteristicReadDto>(
      historicCharacteristic
    );
    return CreatedAtAction(
      nameof(GetHistoricCharacteristic),
      new { id = historicCharacteristic.Id },
      historicCharacteristicReadDto
    );
  }

  [HttpPut("{id}")]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> PutHistoricCharacteristic(
    [FromRoute] Guid id,
    [FromBody] HistoricCharacteristicUpdateDto historicCharacteristicUpdateDto
  )
  {
    var historicCharacteristic = await TryGetHistoricCharacteristicAsync(id);
    if (historicCharacteristic == null)
    {
      return NotFound();
    }

    _mapper.Map(historicCharacteristicUpdateDto, historicCharacteristic);
    _context.Entry(historicCharacteristic).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!HistoricCharacteristicExists(id))
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
  public async Task<IActionResult> DeleteHistoricCharacteristic([FromRoute] Guid id)
  {
    var historicCharacteristic = await TryGetHistoricCharacteristicAsync(id);
    if (historicCharacteristic == null)
    {
      return NotFound();
    }

    _context.HistoricCharacteristics.Remove(historicCharacteristic);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool HistoricCharacteristicExists(Guid id)
  {
    return _context.HistoricCharacteristics.Any(historicCharacteristic =>
      historicCharacteristic.Id == id
    );
  }

  private async Task<HistoricCharacteristic?> TryGetHistoricCharacteristicAsync(Guid id)
  {
    return await _context.HistoricCharacteristics.FindAsync(id);
  }
}
