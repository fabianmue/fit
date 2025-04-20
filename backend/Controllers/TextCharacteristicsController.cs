using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitBackend;

[Route("[controller]")]
[ApiController]
public class TextCharacteristicsController(FitBackendContext context, IMapper mapper)
  : CharacteristicsController<
    TextCharacteristic,
    TextCharacteristicReadDto,
    TextCharacteristicCreateDto,
    TextCharacteristicUpdateDto
  >(context, mapper)
{
  protected override IQueryable<TextCharacteristic> AddDefaultIncludes(
    IQueryable<TextCharacteristic> characteristics
  )
  {
    return characteristics.Include(characteristic => characteristic.CompanyTextCharacteristics);
  }
}
