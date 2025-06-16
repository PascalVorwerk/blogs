using System.ComponentModel.DataAnnotations;
using Business.Contants;
using Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Dtos;

public class CreateUserRequest : IValidatableObject
{
    [Required]
    [StringLength(AppConstants.EmailMaxLength, MinimumLength = 0)]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [StringLength(AppConstants.NameMaxLength, MinimumLength = 0)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(AppConstants.SurnameMaxLength, MinimumLength = 0)]    
    public string Surname { get; set; } = null!;
    
    [Required]
    [Range(AppConstants.AgeMinValue, AppConstants.AgeMaxValue)]
    public int Age { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // The userService is resolved on the server side, from the Service provider.
        var userService = validationContext.GetService<IUserService>();
        
        // Check if the email already exists
        var emailExists = userService?.GetUserByEmail(Email);
        
        if (emailExists != null)
        {
            yield return new ValidationResult("Email already exists", [ nameof(Email) ]);
        }

        yield return new ValidationResult("Always return this validation message", [string.Empty]);
    }
}