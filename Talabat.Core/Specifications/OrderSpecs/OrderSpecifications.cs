﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Specifications;

namespace Talabat.Core.specifications.OrderSpecs
{
	public class OrderSpecifications: BaseSpecifications<Order>
	{
		public OrderSpecifications(string email):base(Order=>Order.BuyerEmail == email) 
		{
			AddInclides();
			AddOrderByDesc(order => order.OrderDate);
		}

		public OrderSpecifications(string email, int Id):base(Order => (Order.BuyerEmail == email && Order.Id == Id))
		{
			AddInclides();
		}
		private void AddInclides()
		{
			Includes.Add(order => order.Items);
			Includes.Add(order => order.DelivreyMethod);


		}
	}
}
