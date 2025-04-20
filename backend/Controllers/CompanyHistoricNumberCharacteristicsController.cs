using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyHistoricNumberCharacteristicsController(
  FitBackendContext context,
  IMapper mapper
)
  : CompanyCharacteristicsController<
    CompanyHistoricNumberCharacteristic,
    HistoricNumberCharacteristic,
    CompanyHistoricNumberCharacteristicCreateDto,
    CompanyHistoricNumberCharacteristicUpdateDto
  >(context, mapper)
{
  protected override async Task<bool> AllRelatedEntitiesExistAsync(
    CompanyHistoricNumberCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return await _context
        .Set<Company>()
        .AnyAsync(company => company.Id == companyCharacteristicCreateDto.CompanyId)
      && await _context
        .Set<HistoricNumberCharacteristic>()
        .AnyAsync(characteristic =>
          characteristic.Id == companyCharacteristicCreateDto.HistoricNumberCharacteristicId
        );
  }

  protected override Task<bool> CompanyCharacteristicWithSameParentsExistsAsync(
    CompanyHistoricNumberCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return _context
      .Set<CompanyHistoricNumberCharacteristic>()
      .AnyAsync(companyCharacteristic =>
        companyCharacteristic.CompanyId == companyCharacteristicCreateDto.CompanyId
        && companyCharacteristic.HistoricNumberCharacteristicId
          == companyCharacteristicCreateDto.HistoricNumberCharacteristicId
      );
  }

  protected override void ClearHistoricValuesIfNeeded(
    CompanyHistoricNumberCharacteristic companyCharacteristic
  )
  {
    companyCharacteristic.Values.Clear();
  }

  protected override IQueryable<CompanyHistoricNumberCharacteristic> AddDefaultIncludes(
    IQueryable<CompanyHistoricNumberCharacteristic> companyCharacteristics
  )
  {
    return companyCharacteristics
      .Include(companyCharacteristic => companyCharacteristic.HistoricNumberCharacteristic)
      .Include(companyCharacteristic => companyCharacteristic.Values);
  }
}
