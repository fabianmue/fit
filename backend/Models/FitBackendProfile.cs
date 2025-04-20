using AutoMapper;

namespace FitBackend;

public class FitBackendProfile : Profile
{
  public FitBackendProfile()
  {
    CreateMap<Company, CompanyReadDto>();
    CreateMap<CompanyCreateDto, Company>()
      .ForMember(
        company => company.StockExchangeCurrency,
        opt =>
          opt.MapFrom(companyCreateDto =>
            MapStockExchangeToCurrency(companyCreateDto.StockExchange)
          )
      );
    CreateMap<CompanyUpdateDto, Company>()
      .ForMember(
        company => company.StockExchangeCurrency,
        opt =>
          opt.MapFrom(companyUpdateDto =>
            MapStockExchangeToCurrency(companyUpdateDto.StockExchange)
          )
      );

    CreateMap<TextCharacteristic, TextCharacteristicReadDto>()
      .ForMember(
        characteristicReadDto => characteristicReadDto.AssociatedCompanyCharacteristicCount,
        opt => opt.MapFrom(characteristic => characteristic.CompanyTextCharacteristics.Count)
      );
    CreateMap<TextCharacteristicCreateDto, TextCharacteristic>();
    CreateMap<TextCharacteristicUpdateDto, TextCharacteristic>();

    CreateMap<NumberCharacteristic, NumberCharacteristicReadDto>()
      .ForMember(
        characteristicReadDto => characteristicReadDto.AssociatedCompanyCharacteristicCount,
        opt => opt.MapFrom(characteristic => characteristic.CompanyNumberCharacteristics.Count)
      );
    CreateMap<NumberCharacteristicCreateDto, NumberCharacteristic>();
    CreateMap<NumberCharacteristicUpdateDto, NumberCharacteristic>();

    CreateMap<HistoricNumberCharacteristic, HistoricNumberCharacteristicReadDto>()
      .ForMember(
        characteristicReadDto => characteristicReadDto.AssociatedCompanyCharacteristicCount,
        opt =>
          opt.MapFrom(characteristic => characteristic.CompanyHistoricNumberCharacteristics.Count)
      );
    CreateMap<HistoricNumberCharacteristicCreateDto, HistoricNumberCharacteristic>();
    CreateMap<HistoricNumberCharacteristicUpdateDto, HistoricNumberCharacteristic>();

    CreateMap<HistoricCurrencyCharacteristic, HistoricCurrencyCharacteristicReadDto>()
      .ForMember(
        characteristicReadDto => characteristicReadDto.AssociatedCompanyCharacteristicCount,
        opt =>
          opt.MapFrom(characteristic => characteristic.CompanyHistoricCurrencyCharacteristics.Count)
      );
    CreateMap<HistoricCurrencyCharacteristicCreateDto, HistoricCurrencyCharacteristic>();
    CreateMap<HistoricCurrencyCharacteristicUpdateDto, HistoricCurrencyCharacteristic>();

    CreateMap<CompanyTextCharacteristic, CompanyTextCharacteristicReadDto>()
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Label,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.TextCharacteristic.Label)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Color,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.TextCharacteristic.Color)
      );
    CreateMap<CompanyTextCharacteristicCreateDto, CompanyTextCharacteristic>();
    CreateMap<CompanyTextCharacteristicUpdateDto, CompanyTextCharacteristic>();

    CreateMap<CompanyNumberCharacteristic, CompanyNumberCharacteristicReadDto>()
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Label,
        opt =>
          opt.MapFrom(companyCharacteristic => companyCharacteristic.NumberCharacteristic.Label)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Color,
        opt =>
          opt.MapFrom(companyCharacteristic => companyCharacteristic.NumberCharacteristic.Color)
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Unit,
        opt => opt.MapFrom(companyCharacteristic => companyCharacteristic.NumberCharacteristic.Unit)
      );
    CreateMap<CompanyNumberCharacteristicCreateDto, CompanyNumberCharacteristic>();
    CreateMap<CompanyNumberCharacteristicUpdateDto, CompanyNumberCharacteristic>();

    CreateMap<CompanyHistoricNumberCharacteristic, CompanyHistoricNumberCharacteristicReadDto>()
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Label,
        opt =>
          opt.MapFrom(companyCharacteristic =>
            companyCharacteristic.HistoricNumberCharacteristic.Label
          )
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Color,
        opt =>
          opt.MapFrom(companyCharacteristic =>
            companyCharacteristic.HistoricNumberCharacteristic.Color
          )
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Unit,
        opt =>
          opt.MapFrom(companyCharacteristic =>
            companyCharacteristic.HistoricNumberCharacteristic.Unit
          )
      );
    CreateMap<CompanyHistoricNumberCharacteristicCreateDto, CompanyHistoricNumberCharacteristic>();
    CreateMap<CompanyHistoricNumberCharacteristicUpdateDto, CompanyHistoricNumberCharacteristic>();

    CreateMap<CompanyHistoricCurrencyCharacteristic, CompanyHistoricCurrencyCharacteristicReadDto>()
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Label,
        opt =>
          opt.MapFrom(companyCharacteristic =>
            companyCharacteristic.HistoricCurrencyCharacteristic.Label
          )
      )
      .ForMember(
        companyCharacteristicReadDto => companyCharacteristicReadDto.Color,
        opt =>
          opt.MapFrom(companyCharacteristic =>
            companyCharacteristic.HistoricCurrencyCharacteristic.Color
          )
      );
    CreateMap<
      CompanyHistoricCurrencyCharacteristicCreateDto,
      CompanyHistoricCurrencyCharacteristic
    >();
    CreateMap<
      CompanyHistoricCurrencyCharacteristicUpdateDto,
      CompanyHistoricCurrencyCharacteristic
    >();

    CreateMap<HistoricValue, HistoricValueReadDto>();
    CreateMap<HistoricValueCreateDto, HistoricValue>();
  }

  private static Currency MapStockExchangeToCurrency(StockExchange? stockExchange)
  {
    return stockExchange switch
    {
      StockExchange.SWX => Currency.CHF,
      StockExchange.NYSE => Currency.USD,
      StockExchange.NASDAQ => Currency.USD,
      StockExchange.LON => Currency.GBP,
      StockExchange.ETR => Currency.EUR,
      _ => throw new ArgumentOutOfRangeException(
        nameof(stockExchange),
        stockExchange,
        "Stock exchange cannot be mapped to a currency"
      ),
    };
  }
}
