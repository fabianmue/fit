using Bogus;

namespace FIT.FitApi.Test;

public class CompanyChangeDtoFaker : Faker<CompanyChangeDto>
{
    public CompanyChangeDtoFaker()
    {
        RuleFor(x => x.Name, f => f.Company.CompanyName());
        RuleFor(x => x.NextReportingDate, f => DateOnly.FromDateTime(f.Date.Future()));
        RuleFor(x => x.ReportingMultiplier, f => f.PickRandom(1000, 1000000));
        RuleFor(x => x.ReportingCurrency, f => f.PickRandom("CHF", "USD", "EUR"));
        RuleFor(x => x.ShareCurrency, f => f.PickRandom("CHF", "USD", "EUR"));
        RuleFor(x => x.ShareIsin, f => $"{f.Address.CountryCode()}{f.Random.Digits(10)}");
        RuleFor(x => x.ShareSymbol, f => f.Random.String2(4).ToUpper());
        RuleFor(x => x.DividendCurrency, f => f.PickRandom("CHF", "USD", "EUR"));
    }
}
