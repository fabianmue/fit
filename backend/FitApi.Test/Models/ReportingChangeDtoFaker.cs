using Bogus;

namespace FIT.FitApi.Test;

public class ReportingChangeDtoFaker : Faker<ReportingChangeDto>
{
    public ReportingChangeDtoFaker()
    {
        RuleFor(
            r => r.PeriodStart,
            f =>
            {
                var year = f.PickRandom(2023, 2024, 2025);
                var month = f.PickRandom(1, 4, 7, 10);
                return DateOnly.FromDateTime(new DateTime(year, month, 1).AddDays(-1));
            }
        );
        RuleFor(
            r => r.PeriodEnd,
            (f, r) => f.Random.Bool() ? r.PeriodStart.AddMonths(3) : r.PeriodStart.AddMonths(6)
        );
        RuleFor(r => r.Comment, f => f.Random.Bool(0.3f) ? f.Lorem.Paragraph() : null);
        RuleFor(r => r.Revenue, f => f.Random.Int(0, 999));
        RuleFor(r => r.Earnings, (f, r) => f.Random.Int(-r.Revenue, r.Revenue / 2));
        RuleFor(r => r.EarningsPerShare, f => Math.Round(f.Random.Double(-99, 99), 2));
        RuleFor(r => r.TotalAssets, f => f.Random.Int(0, 9999));
        RuleFor(r => r.TotalLiabilities, (f, r) => f.Random.Int(0, r.TotalAssets));
    }
}
