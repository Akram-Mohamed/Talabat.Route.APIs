﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries.Contract;
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
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}