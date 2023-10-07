using BE_Shop.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class AddOrder
	{
		//public Guid UserId { get; set; } = Guid.Empty;
		public string Address { get; set; } = string.Empty;
		public List<AddOrderDetail> OrderDetail { get; set; } = new List<AddOrderDetail>();
		internal Guid UserId { get; set; } = Guid.Empty;
	}
	public class AddOrderDetail
	{
		public Guid ProductDetailId { get; set; } = Guid.Empty;
		public int ItemCount { get; set; } = 0;
	}
	public class OutputAddOrder : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		internal override void Query_DataInput(object? ip)
		{
			AddOrder input = (AddOrder)ip;
			List<OrderDetail> OrderDetail = new List<OrderDetail>();
			using (var db = new DatabaseConnection())
			{
				db._Order.Add(new Order()
				{
					Id = Id,
					UserId = input.UserId,
					Address = input.Address,
					CreatedDate = DateTime.Now,
				});
				foreach (var detail in input.OrderDetail)
				{
					db._OrderDetail.Add(new OrderDetail()
					{
						OrderId = Id,
						ProductId = detail.ProductDetailId,
						ItemCount = detail.ItemCount,
						UnitPrice = db._Product.Where(e => e.Id == detail.ProductDetailId).FirstOrDefault()?.UnitPrice ?? throw new HttpException(detail.ProductDetailId.ToString(), 404),
					});
				}
				db.SaveChanges();
			}
		}
	}
}