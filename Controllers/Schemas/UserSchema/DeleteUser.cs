using BE_Shop.Data;
using System.Net;

namespace BE_Shop.Controllers
{
    public class OutputDeleteUser : Output
    {
        internal override void Query_DataInput(object? ip)
        {
			Guid Id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				var user = db._User.Find(Id) ?? throw new HttpException(string.Empty, 404);
				//var address = db._Address.Where(b => b.UserId == Id).ToList();
				//db._Address.RemoveRange(address);
				db._User.Remove(user);
				db.SaveChanges();
			}
		}
    }
}