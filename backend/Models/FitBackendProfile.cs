using AutoMapper;

namespace FitBackend;

public class FitBackendProfile : Profile
{
  public FitBackendProfile()
  {
    CreateMap<Company, CompanyReadDto>();
    CreateMap<CompanyCreateDto, Company>();
    CreateMap<CompanyUpdateDto, Company>();

    CreateMap<Characteristic, CharacteristicReadDto>()
      .ForMember(
        characteristicReadDto => characteristicReadDto.AssociatedCompanyCharacteristicCount,
        opt => opt.MapFrom(characteristic => characteristic.CompanyCharacteristics.Count)
      );
    CreateMap<CharacteristicCreateDto, Characteristic>();
    CreateMap<CharacteristicUpdateDto, Characteristic>();

    CreateMap<CompanyCharacteristic, CompanyCharacteristicReadDto>()
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Icon,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.Characteristic.Icon)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Color,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.Characteristic.Color)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Label,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.Characteristic.Label)
      );
    CreateMap<CompanyCharacteristicCreateDto, CompanyCharacteristic>();
    CreateMap<CompanyCharacteristicUpdateDto, CompanyCharacteristic>();
  }
}
