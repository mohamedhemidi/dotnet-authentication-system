using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Repositories;
using backend_core.Domain.Entities;
using backend_core.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext db) : base(db)
        {
           
        }

    }
}