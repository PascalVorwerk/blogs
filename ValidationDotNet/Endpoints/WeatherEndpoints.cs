using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ValidationDotNet.Filters;
using ValidationDotNet.Models;

namespace ValidationDotNet.Endpoints;

public static class WeatherEndpoints
{
    public static IEndpointRouteBuilder MapWeatherEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/weatherforecasts")
            .WithTags("WeatherForecast");
        
        // Get all weather forecasts
        group.MapGet("/", () =>
        {
            var forecasts = WeatherData.GetWeatherForecasts();
            return Results.Ok(forecasts);
        });
        
        // Create a new weather forecast
        group.MapPost("/", ([FromBody] CreateWeatherForecastRequest request) =>
            {
                // At this point, validation has happened and the request is validated.
                var date = request.Date;
                var weatherForecast = new WeatherForecast(new DateOnly(date.Year, date.Month, date.Day),
                    request.TemperatureC, request.Summary);
                WeatherData.AddWeatherForecast(weatherForecast);
                return Results.Created($"/api/weatherforecasts/{request.Date}", weatherForecast);
            })
            .AddEndpointFilter<ValidationFilter<CreateWeatherForecastRequest>>(); // In dotnet 10, we don't need this filter anymore for minimal API's!
        
        // Update an existing weather forecast
        group.MapPut("/{date:datetime}", ([FromRoute] DateTime date, [FromBody] UpdateWeatherForecastRequest request) =>
        {
            // Because no validation filter was added to the route, it is not validated. We can however still manually validate the object..
            var validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(request, validationContext, validationResults, true);
            
            if (!isValid)
            {
                return Results.ValidationProblem(
                    validationResults.ToDictionary<ValidationResult, string, string[]>(v => v.MemberNames.First(), v => [v.ErrorMessage ?? "Not provided"]));
            }
            
            
            var weatherForecast = new WeatherForecast(new DateOnly(date.Year, date.Month, date.Day), request.TemperatureC, request.Summary);
            WeatherData.UpdateWeatherForecast(weatherForecast);
            return Results.NoContent();
        });
        
        // Delete a weather forecast
        group.MapDelete("/{date:datetime}", ([FromRoute] DateTime date) =>
        {
            WeatherData.DeleteWeatherForecast(new DateOnly(date.Year, date.Month, date.Day));
            return Results.NoContent();
        });
        
        return endpoints;
    }
}