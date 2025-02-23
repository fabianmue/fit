using FitBackend;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(FitBackendProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<FitBackendContext>();
builder.Services.AddHttpLogging(options => { });
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
