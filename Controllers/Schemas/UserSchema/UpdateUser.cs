using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
    public class UpdateUser
    {
		public string Name { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
    public class OutputUpdateUser : Output
    {
        internal override void Query_DataInput(object? ip)
        {
			UpdateUser input = (UpdateUser)ip;
			using (var db = new DatabaseConnection())
			{
				var i = db._User.Where(e => e.UserName == input.UserName).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				i.Name = input.Name;
				i.Password = Converter.MD5Convert(input.Password);
				db.SaveChanges();
			}
		}
    }
}