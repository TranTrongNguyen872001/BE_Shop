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
				User = db._User.Find(Id) ?? throw new HttpException(string.Empty, 404);
				User.AddressList = db._Address.Where(e => e.UserId == Id).ToList();
				User.OrderList = db._Order.Where(e => e.UserId == Id).ToList();
			}
		}
	}
}