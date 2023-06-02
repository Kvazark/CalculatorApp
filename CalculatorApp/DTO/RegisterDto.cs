using System.ComponentModel.DataAnnotations;

namespace CalculatorApp.DTO;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}