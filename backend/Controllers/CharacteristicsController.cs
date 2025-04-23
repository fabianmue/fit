using AutoMapper;
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
  public async Task<ActionResult<CharacteristicsCollectionReadDto>> GetCharacteristics()
  {
    var historicFinancialCharacteristics = await _context
      .Set<HistoricFinancialCharacteristic>()
      .Include(characteristic => characteristic.CompanyHistoricFinancialCharacteristics)
      .AsNoTracking()
      .ToListAsync();
    var historicNumberCharacteristics = await _context
      .Set<HistoricNumberCharacteristic>()
      .Include(characteristic => characteristic.CompanyHistoricNumberCharacteristics)
      .AsNoTracking()
      .ToListAsync();
    var numberCharacteristics = await _context
      .Set<NumberCharacteristic>()
      .Include(characteristic => characteristic.CompanyNumberCharacteristics)
      .AsNoTracking()
      .ToListAsync();
    var textCharacteristics = await _context
      .Set<TextCharacteristic>()
      .Include(characteristic => characteristic.CompanyTextCharacteristics)
      .AsNoTracking()
      .ToListAsync();

    return Ok(
      new CharacteristicsCollectionReadDto
      {
        HistoricFinancialCharacteristics = _mapper.Map<
          List<HistoricFinancialCharacteristicReadDto>
        >(historicFinancialCharacteristics),
        HistoricNumberCharacteristics = _mapper.Map<List<HistoricNumberCharacteristicReadDto>>(
          historicNumberCharacteristics
        ),
        NumberCharacteristics = _mapper.Map<List<NumberCharacteristicReadDto>>(
          numberCharacteristics
        ),
        TextCharacteristics = _mapper.Map<List<TextCharacteristicReadDto>>(textCharacteristics),
      }
    );
  }
}
