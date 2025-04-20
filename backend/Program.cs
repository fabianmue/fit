using System.Text.Json.Serialization;
using FitBackend;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder
  .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.Authority = Environment.GetEnvironmentVariable("FIT_IDENTITY_BASEURL");
    options.Audience = "fit-app";
    options.RequireHttpsMetadata = builder.Environment.IsProduction();
  });
builder
  .Services.AddAuthorizationBuilder()
  .AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
builder.Services.AddAutoMapper(typeof(FitBackendProfile));
builder
  .Services.AddControllers()
  .AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
  );
builder.Services.AddDbContext<FitBackendContext>(options =>
  options.UseNpgsql(Environment.GetEnvironmentVariable("FIT_DATABASE_CONNECTIONSTRING"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpLogging();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();
app.UseSwagger();
app.MapControllers();
app.Run();
