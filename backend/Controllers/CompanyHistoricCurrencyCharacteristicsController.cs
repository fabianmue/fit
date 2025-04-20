using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyHistoricCurrencyCharacteristicsController(
  FitBackendContext context,
  IMapper mapper
)
  : CompanyCharacteristicsController<
    CompanyHistoricCurrencyCharacteristic,
    HistoricCurrencyCharacteristic,
    CompanyHistoricCurrencyCharacteristicCreateDto,
    CompanyHistoricCurrencyCharacteristicUpdateDto
  >(context, mapper)
{
  protected override async Task<bool> AllRelatedEntitiesExistAsync(
    CompanyHistoricCurrencyCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return await _context
        .Set<Company>()
        .AnyAsync(company => company.Id == companyCharacteristicCreateDto.CompanyId)
      && await _context
        .Set<HistoricCurrencyCharacteristic>()
        .AnyAsync(characteristic =>
          characteristic.Id == companyCharacteristicCreateDto.HistoricCurrencyCharacteristicId
        );
  }

  protected override Task<bool> CompanyCharacteristicWithSameParentsExistsAsync(
    CompanyHistoricCurrencyCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return _context
      .Set<CompanyHistoricCurrencyCharacteristic>()
      .AnyAsync(companyCharacteristic =>
        companyCharacteristic.CompanyId == companyCharacteristicCreateDto.CompanyId
        && companyCharacteristic.HistoricCurrencyCharacteristicId
          == companyCharacteristicCreateDto.HistoricCurrencyCharacteristicId
      );
  }

  protected override void ClearHistoricValuesIfNeeded(
    CompanyHistoricCurrencyCharacteristic companyCharacteristic
  )
  {
    companyCharacteristic.Values.Clear();
  }

  protected override IQueryable<CompanyHistoricCurrencyCharacteristic> AddDefaultIncludes(
    IQueryable<CompanyHistoricCurrencyCharacteristic> companyCharacteristics
  )
  {
    return companyCharacteristics
      .Include(companyCharacteristic => companyCharacteristic.HistoricCurrencyCharacteristic)
      .Include(companyCharacteristic => companyCharacteristic.Values);
  }
}
