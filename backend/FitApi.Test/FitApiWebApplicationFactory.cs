using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FIT.FitApi.Test;

public class FitApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextOptionsDescriptor = services.Single(d =>
                d.ServiceType == typeof(DbContextOptions<FitApiContext>)
            );
            services.Remove(dbContextOptionsDescriptor);

            var dbContextDescriptor = services.Single(d => d.ServiceType == typeof(FitApiContext));
            services.Remove(dbContextDescriptor);

            services.AddDbContextFactory<FitApiContext>(options =>
            {
                options.UseSqlite("DataSource=:memory:");
                options.EnableDetailedErrors(true).EnableSensitiveDataLogging(true);
            });

            services.AddSingleton(service =>
            {
                var factory = service.GetRequiredService<IDbContextFactory<FitApiContext>>();
                var context = factory.CreateDbContext();
                context.Database.OpenConnection();
                context.Database.Migrate();
                return context;
            });
        });

        builder.UseEnvironment("Local");
    }
}
