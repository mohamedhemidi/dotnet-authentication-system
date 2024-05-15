using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Application.Contracts.Persistance;
using backend_core.Contracts.Persistance;
using backend_core.Domain.Entities;

namespace backend_core.Infrastructure.Persistence
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        
    }
}