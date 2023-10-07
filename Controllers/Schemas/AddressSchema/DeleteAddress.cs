using BE_Shop.Data;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class DeleteAddress
	{
		internal Guid UserId { get; set; } = Guid.Empty;
		internal Guid AddressId { get; set; } = Guid.Empty;
	}
	public class OutputDeleteAddress : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			DeleteAddress input = (DeleteAddress)ip;
			using (var db = new DatabaseConnection())
			{
				var Address = db._Address.Find(input.AddressId) ?? throw new HttpException("Id không tìm thấy", 404);
				if (Address.UserId != input.UserId)
				{
					throw new HttpException(string.Empty, 403);
				}
				db._Address.Remove(Address);
				db.SaveChanges();
			}
		}
	}
}