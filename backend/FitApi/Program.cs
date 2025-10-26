using FIT.FitApi;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<FitApiContext>(options =>
    options.UseSqlite(
        $"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/fit.db"
    )
);
builder.Services.AddMapster();
builder.Services.AddOpenApi(
    "default",
    options =>
    {
        options.AddDocumentTransformer(
            (document, context, cancellationToken) =>
            {
                document.Info = new() { Title = "FitApi", Version = "1" };
                document.Servers =
                [
                    new Microsoft.OpenApi.Models.OpenApiServer { Url = "http://localhost" },
                ];
                return Task.CompletedTask;
            }
        );
    }
);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();
app.MapOpenApi();
app.MapControllers();

app.Run();

public partial class Program { }
