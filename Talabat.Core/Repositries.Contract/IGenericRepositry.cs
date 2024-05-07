﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;


namespace Talabat.Core.Repositries.Contract
{
	public interface IGenericRepositry<T> where T : BaseEntitiy
	{

		Task<T> GetAsync(int id);
		Task<IEnumerable<T>> GetAllAsync();


		Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
		Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

	}
}
