using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputDeleteProduct : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			Guid Id =(Guid)ip;
			using (var db = new DatabaseConnection())
			{
				var Product = db._Product.Where(e =>  e.Id == Id).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				db._Product.Remove(Product);
				db.SaveChanges();
			}
		}
	}
}