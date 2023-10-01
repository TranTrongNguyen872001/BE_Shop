using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetOneUser : Output
	{
		public User User { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				User = db._User.Where(e => e.Id == Id).ToList().FirstOrDefault();
				User.AddressList = db._Address.Where(e => e.UserId == Id).ToList();
			}
		}
	}
}