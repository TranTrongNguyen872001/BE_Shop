using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputDeleteFileManager : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			Guid input = (Guid)ip;
			using(var db = new DatabaseConnection())
			{
				db._FileManager.Remove(db._FileManager.Find(input) ?? throw new HttpException(string.Empty, 404));
				db.SaveChanges();
			}
		}
	}
}
