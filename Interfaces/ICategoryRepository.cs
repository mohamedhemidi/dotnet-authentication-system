using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Models;

namespace backend_core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // Task Update(Category data);
        Task<List<Category>> GetAllWithInclude();
        Task<Category> GetWithInclude(Expression<Func<Category, bool>> filter);
        Task<bool> CategoryExists(int id);
        Task Save();
    }
}