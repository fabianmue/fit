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

    CreateMap<HistoricCharacteristic, HistoricCharacteristicReadDto>()
      .ForMember(
        historicCharacteristicReadDto =>
          historicCharacteristicReadDto.AssociatedCompanyHistoricCharacteristicCount,
        opt =>
          opt.MapFrom(historicCharacteristic =>
            historicCharacteristic.CompanyHistoricCharacteristics.Count
          )
      );
    CreateMap<HistoricCharacteristicCreateDto, HistoricCharacteristic>();
    CreateMap<HistoricCharacteristicUpdateDto, HistoricCharacteristic>();

    CreateMap<CompanyCharacteristic, CompanyCharacteristicReadDto>()
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Type,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.Characteristic.Type)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Label,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.Characteristic.Label)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Color,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.Characteristic.Color)
      );
    CreateMap<CompanyCharacteristicCreateDto, CompanyCharacteristic>();
    CreateMap<CompanyCharacteristicUpdateDto, CompanyCharacteristic>();

    CreateMap<CompanyHistoricCharacteristic, CompanyHistoricCharacteristicReadDto>()
      .ForMember(
        companyHistoricCharacteristicReadDto => companyHistoricCharacteristicReadDto.Type,
        opt =>
          opt.MapFrom(companyHistoricCharacteristic =>
            companyHistoricCharacteristic.HistoricCharacteristic.Type
          )
      )
      .ForMember(
        companyHistoricCharacteristicReadDto => companyHistoricCharacteristicReadDto.Label,
        opt =>
          opt.MapFrom(companyHistoricCharacteristic =>
            companyHistoricCharacteristic.HistoricCharacteristic.Label
          )
      )
      .ForMember(
        companyHistoricCharacteristicReadDto => companyHistoricCharacteristicReadDto.Color,
        opt =>
          opt.MapFrom(companyHistoricCharacteristic =>
            companyHistoricCharacteristic.HistoricCharacteristic.Color
          )
      );
    CreateMap<CompanyHistoricCharacteristicCreateDto, CompanyHistoricCharacteristic>();
    CreateMap<CompanyHistoricCharacteristicUpdateDto, CompanyHistoricCharacteristic>();

    CreateMap<HistoricValue, HistoricValueReadDto>();
    CreateMap<HistoricValueCreateDto, HistoricValue>();
  }
}
