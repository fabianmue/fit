using Microsoft.EntityFrameworkCore;

namespace FitBackend;

public class FitBackendContext(DbContextOptions<FitBackendContext> options) : DbContext(options)
{
  public DbSet<Company> Companies { get; set; }

  public DbSet<TextCharacteristic> TextCharacteristics { get; set; }

  public DbSet<NumberCharacteristic> NumberCharacteristics { get; set; }

  public DbSet<HistoricNumberCharacteristic> HistoricNumberCharacteristics { get; set; }

  public DbSet<HistoricCurrencyCharacteristic> HistoricCurrencyCharacteristics { get; set; }

  public DbSet<CompanyTextCharacteristic> CompanyTextCharacteristics { get; set; }

  public DbSet<CompanyNumberCharacteristic> CompanyNumberCharacteristics { get; set; }

  public DbSet<CompanyHistoricNumberCharacteristic> CompanyHistoricNumberCharacteristics { get; set; }

  public DbSet<CompanyHistoricCurrencyCharacteristic> CompanyHistoricCurrencyCharacteristics { get; set; }

  public DbSet<HistoricValue> HistoricValues { get; set; }
}
