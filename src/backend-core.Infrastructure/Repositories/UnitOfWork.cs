using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Domain.Repositories;
using backend_core.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend_core.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly Dictionary<Type, object> _repositories;
        public IUserRepository users { get; }
        public IPostRepository posts { get; }


        public UnitOfWork(ApplicationDbContext db, IUserRepository users, IPostRepository posts)
        {
            _db = db;
            this.users = users;
            this.posts = posts;
            _repositories = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task StartTransactionAsync(CancellationToken cancellationToken)
        {
            await _db.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task SubmitTransactionAsync(CancellationToken cancellationToken)
        {
            await _db.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RevertTransactionAsync(CancellationToken cancellationToken)
        {
            await _db.Database.RollbackTransactionAsync(cancellationToken);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                var result = _repositories[typeof(T)] as IRepository<T>;
                return result!;
            }

            var repository = new Repository<T>(_db);
            _repositories.Add(typeof(T), repository);

            return repository;
        }
    }
}