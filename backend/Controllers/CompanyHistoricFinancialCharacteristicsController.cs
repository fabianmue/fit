using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyHistoricFinancialCharacteristicsController(
  FitBackendContext context,
  IMapper mapper
)
  : CompanyCharacteristicsControllerBase<
    CompanyHistoricFinancialCharacteristic,
    HistoricFinancialCharacteristic,
    CompanyHistoricFinancialCharacteristicReadDto,
    CompanyHistoricFinancialCharacteristicCreateDto,
    CompanyHistoricFinancialCharacteristicUpdateDto
  >(context, mapper)
{
  protected override async Task<bool> AllRelatedEntitiesExistAsync(
    CompanyHistoricFinancialCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return await _context
        .Set<Company>()
        .AnyAsync(company => company.Id == companyCharacteristicCreateDto.CompanyId)
      && await _context
        .Set<HistoricFinancialCharacteristic>()
        .AnyAsync(characteristic =>
          characteristic.Id == companyCharacteristicCreateDto.HistoricFinancialCharacteristicId
        );
  }

  protected override Task<bool> CompanyCharacteristicWithSameParentsExistsAsync(
    CompanyHistoricFinancialCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return _context
      .Set<CompanyHistoricFinancialCharacteristic>()
      .AnyAsync(companyCharacteristic =>
        companyCharacteristic.CompanyId == companyCharacteristicCreateDto.CompanyId
        && companyCharacteristic.HistoricFinancialCharacteristicId
          == companyCharacteristicCreateDto.HistoricFinancialCharacteristicId
      );
  }

  protected override void ClearHistoricValuesIfNeeded(
    CompanyHistoricFinancialCharacteristic companyCharacteristic
  )
  {
    companyCharacteristic.Values.Clear();
  }

  protected override IQueryable<CompanyHistoricFinancialCharacteristic> AddDefaultIncludes(
    IQueryable<CompanyHistoricFinancialCharacteristic> companyCharacteristics
  )
  {
    return companyCharacteristics
      .Include(companyCharacteristic => companyCharacteristic.HistoricFinancialCharacteristic)
      .Include(companyCharacteristic => companyCharacteristic.Values);
  }
}
