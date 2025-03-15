using Microsoft.EntityFrameworkCore;

namespace FitBackend;

public class FitBackendContext(DbContextOptions<FitBackendContext> options) : DbContext(options)
{
  public DbSet<Company> Companies { get; set; }

  public DbSet<Characteristic> Characteristics { get; set; }

  public DbSet<HistoricCharacteristic> HistoricCharacteristics { get; set; }

  public DbSet<CompanyCharacteristic> CompanyCharacteristics { get; set; }

  public DbSet<CompanyHistoricCharacteristic> CompanyHistoricCharacteristics { get; set; }

  public DbSet<HistoricValue> HistoricValues { get; set; }
}
