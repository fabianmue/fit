using Bogus;

namespace FIT.FitApi.Test;

public class CompanyChangeDtoFaker : Faker<CompanyChangeDto>
{
    public CompanyChangeDtoFaker()
    {
        RuleFor(c => c.Name, f => f.Company.CompanyName());
        RuleFor(
            c => c.NextReportingDate,
            f => f.Random.Bool() ? DateOnly.FromDateTime(f.Date.Future()) : null
        );
        RuleFor(c => c.ReportingMultiplier, f => f.PickRandom(1000, 1000000));
        RuleFor(c => c.ReportingCurrency, f => f.PickRandom("CHF", "USD", "EUR"));
        RuleFor(c => c.ShareCurrency, f => f.PickRandom("CHF", "USD", "EUR"));
        RuleFor(c => c.ShareIsin, f => $"{f.Address.CountryCode()}{f.Random.Digits(10)}");
        RuleFor(c => c.ShareSymbol, f => f.Random.String2(4).ToUpper());
        RuleFor(c => c.DividendCurrency, f => f.PickRandom("CHF", "USD", "EUR"));
    }
}
