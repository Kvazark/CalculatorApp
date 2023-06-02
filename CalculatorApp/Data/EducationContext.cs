using System;
using System.Collections.Generic;
using Azure;
using CalculatorApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp
{
    // public class EducationContext : DbContext
    // {
    //     protected readonly IConfiguration Configuration;
    //
    //     public EducationContext(IConfiguration configuration)
    //     {
    //         Configuration = configuration;
    //     }
    //
    //     protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     {
    //         // connect to sql server with connection string from app settings
    //         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    //     }
    //
    //     public DbSet<MatchOperation> MatchOperations { get; set; }
    // }
    public class EducationContext : IdentityDbContext<ApplicationUser>
    {
        public EducationContext(DbContextOptions<EducationContext> options)
            :base(options)
        {
            
        }
    
    
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=lonewald.ru;Initial Catalog=Education;user id=katarina;password=123Parsek;Trust Server Certificate=True");
            }
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Operation>(entity =>
            // {
            //     entity.HasKey(operation => new {operation.Id});
            //     entity.HasData(operations);
            // });
           
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            base.OnModelCreating(modelBuilder);
    
        }
        public DbSet<MatchOperation> MatchOperations { get; set; }
    }
}
