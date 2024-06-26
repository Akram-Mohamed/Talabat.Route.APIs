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

		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetAsync(int id);


		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
		Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
		Task<int> GetCountAsync(ISpecifications<T> spec);
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
