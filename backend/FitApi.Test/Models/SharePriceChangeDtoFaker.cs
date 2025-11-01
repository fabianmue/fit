using Bogus;

namespace FIT.FitApi.Test;

public class SharePriceChangeDtoFaker : Faker<SharePriceChangeDto>
{
    public SharePriceChangeDtoFaker()
    {
        RuleFor(
            s => s.Date,
            f => DateOnly.FromDateTime(f.Date.Between(new DateTime(2022, 1, 1), DateTime.Now))
        );
        RuleFor(s => s.Price, f => Math.Round(f.Random.Double(0.1, 1000.0), 2));
    }
}
