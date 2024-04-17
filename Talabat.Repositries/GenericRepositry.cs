using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
using Talabat.Core.Specifications;
using Talabat.Repositries.Data;


namespace Talabat.Repositries
{
	public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseEntitiy
	{
		private readonly StoreContext _dbContext;

		public GenericRepositry(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return (IEnumerable<T>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
			//return await _dbContext.Set<T>().ToListAsync();
		}



		public async Task<T?> GetAsync(int id)
		{

			return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
			//return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
		}


		 
	}
}
