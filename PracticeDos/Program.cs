using BusinessLogic.Manager;
using Serilog;
using UPB.PracticeDos.Midlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile(
        "appsettings." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json"
    )
    .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetSection("LogFilepath").Value, rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<PatientManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHnadlerMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
