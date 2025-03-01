using Microsoft.EntityFrameworkCore;

namespace FitBackend;

public class FitBackendContext(DbContextOptions<FitBackendContext> options) : DbContext(options)
{
  public DbSet<Company> Companies { get; set; }

  public DbSet<Characteristic> Characteristics { get; set; }

  public DbSet<CompanyCharacteristic> CompanyCharacteristics { get; set; }
}
