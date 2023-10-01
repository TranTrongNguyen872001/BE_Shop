using BE_Shop.Data;
using System.Net;

namespace BE_Shop.Controllers
{
    public class OutputDeleteUser : Output
    {
        internal override void Query_DataInput(object? ip)
        {
			Guid Id = Guid.Parse(ip.ToString());
			using (var db = new DatabaseConnection())
			{
				db._User.RemoveRange(db._User.Where(b => b.Id == Id));
				db.SaveChanges();
			}
		}
    }
}