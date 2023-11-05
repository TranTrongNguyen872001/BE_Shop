using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class UpdateOrder
	{
        internal Guid Id { get; set; } = Guid.Empty;
        internal Guid UserId { get; set; } = Guid.Empty;
        public List<AddOrderDetail> OrderDetail { get; set; } = new List<AddOrderDetail>();
    }
	public class OutputUpdateOrder : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateOrder input = (UpdateOrder)ip!;
			using (var db = new DatabaseConnection())
			{
				var Order = db._Order.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
				if(Order.Status != 0 || Order.UserId != input.UserId)
				{
					throw new HttpException(string.Empty, 403);
                }
                db.RemoveRange(db._OrderDetail.Where(e => e.OrderId == input.Id));
                db.SaveChanges();
                foreach (var detail in input.OrderDetail)
				{
					db._OrderDetail.Add(new OrderDetail()
					{
						OrderId = input.Id,
						ProductId = detail.ProductId,
						ItemCount = detail.ItemCount,
						UnitPrice = db._Product.Where(e => e.Id == detail.ProductId).Select(e => new { e.UnitPrice }).FirstOrDefault()?.UnitPrice ?? throw new HttpException(detail.ProductId.ToString(), 404),
                    });
				}
				db.SaveChanges();
			}
		}
	}
}