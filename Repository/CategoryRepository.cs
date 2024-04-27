using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Data;
using backend_core.Interfaces;
using backend_core.Models;

namespace backend_core.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
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