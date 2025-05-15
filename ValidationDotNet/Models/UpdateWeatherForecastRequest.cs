using System.ComponentModel.DataAnnotations;

namespace ValidationDotNet.Models;

public class UpdateWeatherForecastRequest
{
    [Required]
    [Range(-20, 55)]
    public int TemperatureC { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Summary { get; set; } = string.Empty;
}