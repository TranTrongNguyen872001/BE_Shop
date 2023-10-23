using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class UpdateOrder
	{
		public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public List<AddOrderDetail> OrderDetail { get; set; } = new List<AddOrderDetail>();
    }
	public class OutputUpdateOrder : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateOrder input = (UpdateOrder)ip!;
			using (var db = new DatabaseConnection())
			{
				var Order = db._Order.Where(e => e.Id == input.Id && e.Status == 0).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				Order.Address = input.Address;
                db.SaveChanges();
                db.RemoveRange(db._OrderDetail.Where(e => e.OrderId == input.Id));
                db.SaveChanges();
                foreach (var dt in input.OrderDetail)
				{
					db._OrderDetail.Add(new OrderDetail()
					{
						OrderId = input.Id,
						ProductId = dt.ProductId,
						ItemCount = dt.ItemCount,
						UnitPrice = db._Product.Find(dt.ProductId)?.UnitPrice ?? 0,
					});
				}
				db.SaveChanges();
			}
		}
	}
}