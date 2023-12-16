using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputDeleteDiscount : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			Guid input = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
				var Discount = db._Discount.Find(input) ?? throw new HttpException("Id không tìm thấy", 404);
				Discount.Status = false;
				db.SaveChanges();
			}
		}
	}
}