using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class NumberCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CharacteristicsController<
    NumberCharacteristic,
    NumberCharacteristicReadDto,
    NumberCharacteristicCreateDto,
    NumberCharacteristicUpdateDto
  >(context, mapper)
{
  protected override IQueryable<NumberCharacteristic> AddDefaultIncludes(
    IQueryable<NumberCharacteristic> characteristics
  )
  {
    return characteristics.Include(characteristic => characteristic.CompanyNumberCharacteristics);
  }
}
