using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class HistoricNumberCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CharacteristicsControllerBase<
    HistoricNumberCharacteristic,
    HistoricNumberCharacteristicReadDto,
    HistoricNumberCharacteristicCreateDto,
    HistoricNumberCharacteristicUpdateDto
  >(context, mapper)
{
  protected override IQueryable<HistoricNumberCharacteristic> AddDefaultIncludes(
    IQueryable<HistoricNumberCharacteristic> characteristics
  )
  {
    return characteristics.Include(characteristic =>
      characteristic.CompanyHistoricNumberCharacteristics
    );
  }
}
