using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class CompanyNumberCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CompanyCharacteristicsControllerBase<
    CompanyNumberCharacteristic,
    NumberCharacteristic,
    CompanyNumberCharacteristicReadDto,
    CompanyNumberCharacteristicCreateDto,
    CompanyNumberCharacteristicUpdateDto
  >(context, mapper)
{
  protected override async Task<bool> AllRelatedEntitiesExistAsync(
    CompanyNumberCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return await _context
        .Set<Company>()
        .AnyAsync(company => company.Id == companyCharacteristicCreateDto.CompanyId)
      && await _context
        .Set<NumberCharacteristic>()
        .AnyAsync(characteristic =>
          characteristic.Id == companyCharacteristicCreateDto.NumberCharacteristicId
        );
  }

  protected override Task<bool> CompanyCharacteristicWithSameParentsExistsAsync(
    CompanyNumberCharacteristicCreateDto companyCharacteristicCreateDto
  )
  {
    return _context
      .Set<CompanyNumberCharacteristic>()
      .AnyAsync(companyCharacteristic =>
        companyCharacteristic.CompanyId == companyCharacteristicCreateDto.CompanyId
        && companyCharacteristic.NumberCharacteristicId
          == companyCharacteristicCreateDto.NumberCharacteristicId
      );
  }

  protected override void ClearHistoricValuesIfNeeded(
    CompanyNumberCharacteristic companyCharacteristic
  )
  {
    // No need to clear historic values for company number characteristics
  }

  protected override IQueryable<CompanyNumberCharacteristic> AddDefaultIncludes(
    IQueryable<CompanyNumberCharacteristic> companyCharacteristics
  )
  {
    return companyCharacteristics.Include(companyCharacteristic =>
      companyCharacteristic.NumberCharacteristic
    );
  }
}
