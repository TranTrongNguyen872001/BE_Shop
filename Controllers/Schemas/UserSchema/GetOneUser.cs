using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class OutputGetOneUser : Output
	{
		public object User { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
				this.User = db._User
				.Where(e => e.Id == Id)
				.Select(e => new
				{
					e.Id, e.Name, e.Role, e.UserName,e.Contact,e.Gender,e.Birthday,
                    AddressList = db._Address.Where(y => y.UserId == Id).ToList(),
                    OrderList = db._Order.Where(y => y.UserId == Id).ToList(),
					Status = (e.Role == "NotValid") ? "Unactive" : "Active"
				}).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}