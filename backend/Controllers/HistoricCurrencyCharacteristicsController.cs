using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class HistoricCurrencyCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CharacteristicsController<
    HistoricCurrencyCharacteristic,
    HistoricCurrencyCharacteristicReadDto,
    HistoricCurrencyCharacteristicCreateDto,
    HistoricCurrencyCharacteristicUpdateDto
  >(context, mapper)
{
  protected override IQueryable<HistoricCurrencyCharacteristic> AddDefaultIncludes(
    IQueryable<HistoricCurrencyCharacteristic> characteristics
  )
  {
    return characteristics.Include(characteristic =>
      characteristic.CompanyHistoricCurrencyCharacteristics
    );
  }
}
