using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Core;
using Talabat.Repositries.Data;

namespace Talabat.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public IGenericRepositry<TEntity> Repository<TEntity>() where TEntity : BaseEntitiy
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repo = new GenericRepositry<TEntity>(_dbContext);
                _repositories.Add(key, repo);
            }
            return _repositories[key] as IGenericRepositry<TEntity>;
        }
        public async Task<int> Compelete()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}
