using FitBackend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(FitBackendProfile));
builder.Services.AddControllers();
builder.Services.AddDbContext<FitBackendContext>(options =>
  options.UseNpgsql(Environment.GetEnvironmentVariable("FIT_DATABASE_CONNECTIONSTRING"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpLogging(options => { });
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
