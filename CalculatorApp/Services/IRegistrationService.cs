using CalculatorApp.DTO;

namespace CalculatorApp.Services;

public interface IRegistrationService
{
    public Task<UserDto?> UserRegistration(RegisterDto registerDto);
}