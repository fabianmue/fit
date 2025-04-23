using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyTextCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CompanyCharacteristicsControllerBase<
    CompanyTextCharacteristic,
    TextCharacteristic,
    CompanyTextCharacteristicReadDto,
    CompanyTextCharacteristicCreateDto,
    CompanyTextCharacteristicUpdateDto
  >(context, mapper)
{
  protected override async Task<bool> AllRelatedEntitiesExistAsync(
    CompanyTextCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return await _context
        .Set<Company>()
        .AnyAsync(company => company.Id == companyCharacteristicCreateDto.CompanyId)
      && await _context
        .Set<TextCharacteristic>()
        .AnyAsync(characteristic =>
          characteristic.Id == companyCharacteristicCreateDto.TextCharacteristicId
        );
  }

  protected override Task<bool> CompanyCharacteristicWithSameParentsExistsAsync(
    CompanyTextCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return _context
      .Set<CompanyTextCharacteristic>()
      .AnyAsync(companyCharacteristic =>
        companyCharacteristic.CompanyId == companyCharacteristicCreateDto.CompanyId
        && companyCharacteristic.TextCharacteristicId
          == companyCharacteristicCreateDto.TextCharacteristicId
      );
  }

  protected override void ClearHistoricValuesIfNeeded(
    CompanyTextCharacteristic companyCharacteristic
  )
  {
    // No need to clear historic values for company text characteristics
  }

  protected override IQueryable<CompanyTextCharacteristic> AddDefaultIncludes(
    IQueryable<CompanyTextCharacteristic> companyCharacteristics
  )
  {
    return companyCharacteristics.Include(companyCharacteristic =>
      companyCharacteristic.TextCharacteristic
    );
  }
}
