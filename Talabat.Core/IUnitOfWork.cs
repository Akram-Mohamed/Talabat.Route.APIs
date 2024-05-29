using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepositry<T> Repository<T>() where T : BaseEntitiy;

        public Task<int> Compelete();
    }
}
