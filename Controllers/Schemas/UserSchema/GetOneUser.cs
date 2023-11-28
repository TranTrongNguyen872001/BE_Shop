using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class OutputGetOneUserData
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		public string UserName { get; set; }
		public string? Contact { get; set; }
		public bool? Gender { get; set; }
		public DateTime? Birthday { get; set; }
		public List<Address> AddressList { get; set; }
		public string Status { get; set; }
		public int TotalOrder { get; set; }
		public Guid NewOrderId { get; set; }
		public double TotalSpent  { get; set; }
	}
	public class OutputGetOneUser : Output
	{
		public OutputGetOneUserData User { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
				this.User = db._User
				.Where(e => e.Id == Id)
				.Select(e => new OutputGetOneUserData
				{
					Id = e.Id, 
					Name = e.Name, 
					Role = e.Role, 
					UserName = e.UserName,
					Contact = e.Contact,
					Gender = e.Gender,
					Birthday = e.Birthday,
                    AddressList = db._Address.Where(y => y.UserId == Id).ToList(),
					Status = (e.Status == 0) ? "Active" : "Inactive",
					TotalOrder = db._Order.Where(y => y.UserId == Id).Count(),
					NewOrderId = db._Order.Where(y => y.Status == 0 && y.UserId == Id).FirstOrDefault().Id,
					TotalSpent = db._Order
							.Join(db._OrderDetail, x => x.Id, y => y.OrderId, 
								(x,y)=> new
								{
									x.UserId,
									y.ItemCount,
									y.UnitPrice
								})
							.Where(y => y.UserId == Id)
							.Select(y => y.ItemCount * y.UnitPrice)
							.Sum()
				}).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}