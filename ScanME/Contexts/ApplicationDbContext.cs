using Microsoft.EntityFrameworkCore;
using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyCategory> CompanyCategories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CompanyCategory>().HasData(new CompanyCategory()
            {
                CompanyCategoryId=1,
                Name = "Tourism",
            },
            new CompanyCategory()
            {
                CompanyCategoryId=2,
                Name="Hotel, Restaurant and Bar"
            }
            );

            //add roles
            builder.Entity<Role>().HasData(
                new Role() {RoleId=1, Name="Admin"},
                new Role() {RoleId=2, Name="Content developer"}
                );
           
        }
    }
}
