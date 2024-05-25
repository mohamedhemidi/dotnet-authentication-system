using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace backend_core.Application.Contracts.Persistance
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task Create(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);

        Task Save();
    }
}