using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Application.Contracts.Persistance;
using backend_core.Domain.Entities;
using backend_core.Infrastructure.Persistence.Data;

namespace backend_core.Infrastructure.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}