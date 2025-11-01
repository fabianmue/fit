using Bogus;

namespace FIT.FitApi.Test;

public class DividendChangeDtoFaker : Faker<DividendChangeDto>
{
    public DividendChangeDtoFaker()
    {
        RuleFor(
            d => d.PeriodStart,
            f =>
            {
                var year = f.PickRandom(2022, 2023, 2024);
                return DateOnly.FromDateTime(new DateTime(year, 1, 1));
            }
        );
        RuleFor(d => d.PeriodEnd, (f, d) => d.PeriodStart.AddMonths(12));
        RuleFor(d => d.PayoutDate, (f, d) => d.PeriodEnd.AddDays(f.Random.Int(1, 60)));
        RuleFor(d => d.AmountPerShare, f => Math.Round(f.Random.Double(0.01, 10.0), 2));
    }
}
