using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Domain.Repositories;
using backend_core.Domain.Entities;
using backend_core.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;

namespace backend_core.Infrastructure.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(ApplicationDbContext db, UserManager<AppUser> userManager) : base(db)
        {
            _userManager = userManager;
        }
        public async Task<AppUser> FindByEmailOrUsernameAsync(string userNameOrEmail)
        {
            var userByEmail = await _userManager.FindByEmailAsync(userNameOrEmail);
            if (userByEmail != null)
            {
                return userByEmail;
            }
            var userByName = await _userManager.FindByNameAsync(userNameOrEmail);
            return userByName!;
        }
    }
}