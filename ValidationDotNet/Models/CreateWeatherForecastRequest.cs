using System.ComponentModel.DataAnnotations;

namespace ValidationDotNet.Models;

/// <summary>
/// Create WeatherForecastRequest is a data transfer object (DTO) used to create a new weather forecast.
/// </summary>
public class CreateWeatherForecastRequest : IValidatableObject
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    [Range(-20, 55)]
    public int TemperatureC { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Summary { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Here you can add custom validation logic if needed
        
        // Example: A -20 temperature cannot have a "Hot" summary..
        if (TemperatureC == -20 && Summary.Equals("Hot", StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "A temperature of -20 cannot have a 'Hot' summary.",
                [nameof(Summary)]);
        }
        
        // Or.. the date must be in the future
        if (Date <= DateTime.Now)
        {
            yield return new ValidationResult(
                "The date must be in the future.",
                [nameof(Date)]);
        }
    }
}