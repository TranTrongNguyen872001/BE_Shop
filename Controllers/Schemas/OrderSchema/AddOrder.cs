﻿using BE_Shop.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class AddOrder
	{
        public List<AddOrderDetail> OrderDetail { get; set; } = new List<AddOrderDetail>();
		internal Guid UserId { get; set; } = Guid.Empty;
	}
	public class AddOrderDetail
	{
		public Guid ProductId { get; set; } = Guid.Empty;
		public int ItemCount { get; set; } = 0;
	}
	public class OutputAddOrder : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		internal override void Query_DataInput(object? ip)
		{
			AddOrder input = (AddOrder)ip!;
			List<OrderDetail> OrderDetail = new List<OrderDetail>();
			using (var db = new DatabaseConnection())
			{
                db._Order.Add(new Order()
				{
					Id = this.Id,
					UserId = input.UserId,
					Status = 0,
				});
				foreach (var detail in input.OrderDetail)
				{
					db._OrderDetail.Add(new OrderDetail()
					{
						OrderId = this.Id,
						ProductId = detail.ProductId,
						ItemCount = detail.ItemCount,
						UnitPrice = 0,
					});
				}
				db.SaveChanges();
			}
		}
	}
}