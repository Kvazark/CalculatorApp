using CalculatorApp.DTO;
using CalculatorApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace CalculatorApp.Services;

public class RegistrationService:IRegistrationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
   
    public async Task<UserDto?> UserRegistration(RegisterDto registerDto)
    {
        var user = new ApplicationUser
        {
            DisplayName = registerDto.DisplayName,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Role = "User",
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (result.Succeeded)
        {
            return new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                UserName = user.UserName,
                Token = token,
                Role = user.Role
            };
        }

        return null;
    }
}