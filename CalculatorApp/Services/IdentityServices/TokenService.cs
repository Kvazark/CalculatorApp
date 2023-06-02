using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CalculatorApp.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CalculatorApp.Services.IdentityServices;

public class TokenService
{
    private readonly IConfiguration _configuration;

    private readonly EducationContext _context;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["IdentityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}