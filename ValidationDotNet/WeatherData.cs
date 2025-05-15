namespace ValidationDotNet;

/// <summary>
/// This class simulates some type of data store for weather forecasts.
/// </summary>
public static class WeatherData
{
    private static List<WeatherForecast> _weatherForecasts = new();
    private static string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    public static void Initialize()
    {
        
        _weatherForecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                ))
            .ToList();
    }
    
    
    public static List<WeatherForecast> GetWeatherForecasts()
    {
        return _weatherForecasts;
    }
    
    public static void AddWeatherForecast(WeatherForecast weatherForecast)
    {
        _weatherForecasts.Add(weatherForecast);
    }
    
    public static void UpdateWeatherForecast(WeatherForecast weatherForecast)
    {
        var index = _weatherForecasts.FindIndex(w => w.Date == weatherForecast.Date);
        if (index != -1)
        {
            _weatherForecasts[index] = weatherForecast;
        }
    }
    
    public static void DeleteWeatherForecast(DateOnly date)
    {
        var index = _weatherForecasts.FindIndex(w => w.Date == date);
        if (index != -1)
        {
            _weatherForecasts.RemoveAt(index);
        }
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}