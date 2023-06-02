using CalculatorApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace CalculatorApp;

public class Seed
{
    public static async Task SeedData(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
    {
        
        if (!userManager.Users.Any())
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser {DisplayName = "Bob", UserName = "Bob",Email = "bob@mail.ru",Role = "User"},
                new ApplicationUser {DisplayName = "Tom", UserName = "Tom",Email = "tom@mail.ru",Role = "User"}
            };
    
            foreach (var user in users)
            {
               await userManager.CreateAsync(user, "Password");
            }
            
        }
        
        var appUserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
        
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
    
        if (roleManager != null && !roleManager.RoleExistsAsync("Admin").Result)
        {
            roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
        }
                
        if (appUserManager.FindByEmailAsync("admin@example.com").Result == null)
        {
            var user = new ApplicationUser
            {
                UserName = "Sam",
                DisplayName = "Admin",
                Email = "admin@example.com",
                Role = "Admin"
            };
    
            IdentityResult result = appUserManager.CreateAsync(user, "Password").Result;
    
            if (result.Succeeded)
            {
                appUserManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
        
    }
}