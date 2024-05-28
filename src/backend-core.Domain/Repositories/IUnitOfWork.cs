using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Entities;

namespace backend_core.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository users { get; }
        IPostRepository posts { get; }

        IRepository<T> GetRepository<T>() where T : class;

        Task StartTransactionAsync(CancellationToken cancellationToken);
        Task SubmitTransactionAsync(CancellationToken cancellationToken);
        Task RevertTransactionAsync(CancellationToken cancellationToken);

        Task<int> Save();
    }
}