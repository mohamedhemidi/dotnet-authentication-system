using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Common;
using backend_core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Post> posts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.Updated_at = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created_at = DateTime.Now;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
       
    }
}