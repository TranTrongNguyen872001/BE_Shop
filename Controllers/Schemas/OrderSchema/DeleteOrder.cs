using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputDeleteOrder : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				var order = db._Order.Where(e => e.Id == Id).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				//var orderdetail = db._OrderDetail.Where(e => e.OrderId == Id).ToList();
				db._Order.Remove(order);
				db.SaveChanges();
			}
		}
	}
}