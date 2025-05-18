using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Contants;
using Microsoft.EntityFrameworkCore;

namespace Business.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [StringLength(AppConstants.EmailMaxLength, MinimumLength = 0)]
    public string Email { get; set; } = null!;
    
    [StringLength(AppConstants.NameMaxLength, MinimumLength = 0)]
    public string Name { get; set; } = null!;
    
    [StringLength(AppConstants.SurnameMaxLength, MinimumLength = 0)]
    public string Surname { get; set; } = null!;
    
    [Range(AppConstants.AgeMinValue, AppConstants.AgeMaxValue)]
    public int Age { get; set; }
}