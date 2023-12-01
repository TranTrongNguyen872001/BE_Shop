using BE_Shop.Data;
using Org.BouncyCastle.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
    public class UpdateUserForadmin
    {
		public Guid Id { get; set; } = Guid.Empty;
		public bool Status {get; set;} = true;
    }
    public class OutputUpdateUserForadmin : Output
    {
        internal override void Query_DataInput(object? ip)
        {
			UpdateUserForadmin input = (UpdateUserForadmin)ip!;
			using (var db = new DatabaseConnection())
			{
				var user = db._User.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
				user.Status = input.Status;
                db.SaveChanges();
			}
		}
    }
}