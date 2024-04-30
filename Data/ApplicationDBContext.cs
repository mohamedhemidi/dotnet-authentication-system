using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Skill> skills { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Super Admin",
                    NormalizedName= "SUPER_ADMIN"
                },
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName= "ADMIN"
                },
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName= "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}