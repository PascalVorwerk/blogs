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
        // How do we make sure the email is unique?
        var userService = validationContext.GetRequiredService<IUserService>();

        var emailExists = userService.GetUserByEmailAsync(Email).Result;

        if (emailExists != null)
        {
            yield return new ValidationResult("Email already exists", [nameof(Email)]);
        }
    }
}