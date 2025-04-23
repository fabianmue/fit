using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class HistoricFinancialCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CharacteristicsControllerBase<
    HistoricFinancialCharacteristic,
    HistoricFinancialCharacteristicReadDto,
    HistoricFinancialCharacteristicCreateDto,
    HistoricFinancialCharacteristicUpdateDto
  >(context, mapper)
{
  protected override IQueryable<HistoricFinancialCharacteristic> AddDefaultIncludes(
    IQueryable<HistoricFinancialCharacteristic> characteristics
  )
  {
    return characteristics.Include(characteristic =>
      characteristic.CompanyHistoricFinancialCharacteristics
    );
  }
}
