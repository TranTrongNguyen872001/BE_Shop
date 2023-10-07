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
				db._Product.Remove(db._Product.Find(Id) ?? throw new HttpException(string.Empty, 404));
				db._FileManager.RemoveRange(db._FileManager.Where(e => e.OwnerId == Id).ToList());
				db.SaveChanges();
			}
		}
	}
}