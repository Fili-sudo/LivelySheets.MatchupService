using LivelySheets.MatchupService.API.Extensions;
using LivelySheets.MatchupService.Application.Utils;
using LivelySheets.MatchupService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(config.GetConnectionString("CString")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Helper.AssemblyReference));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
