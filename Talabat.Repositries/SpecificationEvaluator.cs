using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repositries
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity  : BaseEntitiy 
	{
	


	
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
		{
			var query= inputQuery;


            if (spec.Criteria is not null)
				query = query.Where(spec.Criteria);

			// query = _dbContext.Set<Product>().Where(P => P.Id // Includes
			// 1. P => P. Brand
			// 2. P => P.Category

			 spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

			return query;
		}

		
	}
}
