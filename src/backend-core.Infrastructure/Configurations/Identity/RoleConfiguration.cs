using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend_core.Infrastructure.Configurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                 new IdentityRole
                 {
                     Name = UserRoles.SuperAdmin,
                     NormalizedName = "SUPER_ADMIN"
                 },
                new IdentityRole
                {
                    Name = UserRoles.Admin,
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = UserRoles.Moderator,
                    NormalizedName = "MODERATOR"
                },
                new IdentityRole
                {
                    Name = UserRoles.User,
                    NormalizedName = "USER"
                }
             );
        }
    }
}