using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Data;
using backend_core.Helpers;
using backend_core.Interfaces;
using backend_core.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> CategoryExists(Guid id)
        {
            return await _db.categories.AnyAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllWithInclude(QueryObject query)
        {

            var categories = _db.categories.Include(c => c.Skills).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                categories = categories.Where(n => n.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = query.IsDescending ? categories.OrderByDescending(n => n.Name) : categories.OrderBy(n => n.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await categories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Category> GetWithInclude(Expression<Func<Category, bool>> filter)
        {
            return await _db.categories.Where(filter).Include(c => c.Skills).FirstOrDefaultAsync();

        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        // public async Task Update(Category data)
        // {
        //     _db.categories.Update(data);
        // }

    }
}