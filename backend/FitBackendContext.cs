using Microsoft.EntityFrameworkCore;

namespace FitBackend;

public class FitBackendContext : DbContext
{
  public DbSet<Company> Companies { get; set; }

  public DbSet<Characteristic> Characteristics { get; set; }

  public DbSet<CompanyCharacteristic> CompanyCharacteristics { get; set; }

  public string DbPath { get; }

  public FitBackendContext()
  {
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    DbPath = Path.Join(path, "fit_database.db");
  }

  protected override void OnConfiguring(DbContextOptionsBuilder options) =>
    options.UseSqlite($"Data Source={DbPath}");
}
