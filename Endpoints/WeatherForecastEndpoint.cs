namespace ExperimentApi.Endpoints;

public static class WeatherForecastEndpoint
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static void MapWeatherForecastEndpoint(this WebApplication app)
    {
        app.MapGet("/weatherforecast", GetWeatherForecast)
            .WithName("GetWeatherForecast");
    }

    private static WeatherForecast[] GetWeatherForecast(HttpContext httpContext, ILogger<Program> logger)
    {
        // Log each incoming request to this endpoint with useful request info
        logger.LogInformation("GetWeatherForecast request received from {RemoteIp} - {Method} {Path}{QueryString}",
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            httpContext.Request.Method,
            httpContext.Request.Path,
            httpContext.Request.QueryString);

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)]
            ))
            .ToArray();
        return forecast;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
