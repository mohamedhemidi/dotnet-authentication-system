using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Entities;

namespace backend_core.Application.Contracts.Persistance
{
    public interface IPostRepository : IRepository<Post>
    {
        
    }
}