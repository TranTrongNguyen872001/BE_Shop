using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BE_Shop.Controllers
{
	public class AddAddress
	{
		public List<string> Address { get; set; } = new List<string>();
	}
	public class OutputAddAddress : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			var json = JsonConvert.SerializeObject(ip);
			(List<string> Address, Guid UserId) input = JsonConvert.DeserializeObject<(List<string>, Guid)>(json);
			using (var db = new DatabaseConnection())
			{
				var i = db._User.Select(e => new { e.Id }).Where(e => e.Id == input.UserId).ToList().FirstOrDefault() ?? throw new HttpException("Token không hợp lệ", 401);
				foreach(var item in input.Address)
				{
					db._Address.Add(new Address()
					{
						Id = Guid.NewGuid(),
						UserId = input.UserId,
						Description = item
					});
				}
				db.SaveChanges();
			}
		}
	}
}