using BE_Shop.Data;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class OutputDeleteAddress : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			var json = JsonConvert.SerializeObject(ip);
			(Guid AddressId, Guid UserId) input = JsonConvert.DeserializeObject<(Guid , Guid)>(json);
			using (var db = new DatabaseConnection())
			{
				var Address = db._Address.Where(e => e.Id == input.AddressId).FirstOrDefault() ?? throw new HttpException("Id không tìm thấy", 404);
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