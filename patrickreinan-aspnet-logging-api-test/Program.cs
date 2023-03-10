using patrickreinan_aspnet_logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

 builder.Services.AddLogging(c =>
  {

      c.ClearProviders()
      .AddPRConsoleLogger(() =>
      new PRLoggerConfiguration(LogLevel.Information)

      );


  });

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (ILogger<Program> logger) =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();


    logger.LogInformation("teste");
    logger.LogInformation("teste 2");
    return forecast;
});

app.UsePRLogging(nameof(WeatherForecast));
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    
    
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

