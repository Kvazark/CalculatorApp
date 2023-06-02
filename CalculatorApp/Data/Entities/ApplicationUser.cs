using Microsoft.AspNetCore.Identity;

namespace CalculatorApp.Entities;

public class ApplicationUser: IdentityUser
{
    public string DisplayName { get; set; }

    public string Role { get; set; }
    
    public ICollection<MatchOperation> Operations { get; set; } = new List<MatchOperation>();
}