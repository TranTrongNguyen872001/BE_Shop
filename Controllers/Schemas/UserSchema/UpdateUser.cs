using BE_Shop.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
    public class UpdateUser
    {
		internal Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string? Contact { get; set; } = null;
        public bool? Gender { get; set; } = null;
        public DateOnly? Birthday { get; set; } = null;
    }
    public class OutputUpdateUser : Output
    {
        internal override void Query_DataInput(object? ip)
        {
			UpdateUser input = (UpdateUser)ip!;
			using (var db = new DatabaseConnection())
			{
				var user = db._User.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
                if (user.UserName != input.UserName)
				{
					if (db._User.Where(e => e.UserName == input.UserName).Any())
					{
						throw new HttpException(string.Empty, 409);
					}
					user.UserName = input.UserName;
					user.Role = "NotValid";
                }
                user.Name = input.Name;
                user.Password = Converter.MD5Convert(input.Password);
				user.Contact = input.Contact;
				user.Gender = input.Gender;
				user.Birthday = input.Birthday;
                db.SaveChanges();
			}
		}
    }
}