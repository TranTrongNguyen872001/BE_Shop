using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class OutputGetOneUser : Output
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public List<Address> AddressList { get; set; } = new List<Address>();
		public List<Order> OrderList { get; set; } = new List<Order>();

		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				var User = db._User.Find(Id) ?? throw new HttpException(string.Empty, 404);
				this.Id = Id;
				this.Name = User.Name;
				this.Role = User.Role;
				this.UserName = User.UserName;
				this.AddressList = db._Address.Where(e => e.UserId == Id).ToList();
				this.OrderList = db._Order.Where(e => e.UserId == Id).ToList();
			}
		}
	}
}