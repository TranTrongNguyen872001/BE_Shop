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
				db._User.Remove(db._User.Where(b => b.Id == Id).FirstOrDefault() ?? throw new HttpException("Id không tìm thấy", 404));
				db.SaveChanges();
			}
		}
    }
}