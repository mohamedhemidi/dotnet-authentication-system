using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Application.Contracts.Persistance;
using backend_core.Domain.Entities;
using backend_core.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _db.RemoveRange(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}