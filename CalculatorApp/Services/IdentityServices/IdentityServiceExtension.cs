using System.Text;
using CalculatorApp.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CalculatorApp.Services.IdentityServices;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<EducationContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider)
            .AddSignInManager<SignInManager<ApplicationUser>>();
        
        
       // return services;
        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider)
            .AddEntityFrameworkStores<EducationContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();

        // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["IdentityKey"]));
        //
        // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        // {
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = key,
        //         ValidateIssuer = false,
        //         ValidateAudience = false
        //     };
        // } );
        services.AddScoped<TokenService>();
        return services;
        
  
    }
}