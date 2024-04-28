using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend_core.Models;

namespace backend_core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task Create(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }

}