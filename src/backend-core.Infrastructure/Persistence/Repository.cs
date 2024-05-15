using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Contracts.Persistance;
using backend_core.Domain.Entities;

namespace backend_core.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private static readonly List<T> _users = new();
        public async Task Create(T entity)
        {
            _users.Add(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            var user = _users.AsQueryable().FirstOrDefault(filter);
            return await Task.FromResult(user);
        }

        public Task<IReadOnlyList<T>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}