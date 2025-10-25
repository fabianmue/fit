using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi;

public class FitApiContext : DbContext
{
    public DbSet<Company> Companies { get; set; }

    public DbSet<Reporting> Reportings { get; set; }

    public DbSet<SharePrice> SharePrices { get; set; }

    public DbSet<Dividend> Dividends { get; set; }

    public FitApiContext()
        : base() { }

    public FitApiContext(DbContextOptions<FitApiContext> options)
        : base(options) { }
}
