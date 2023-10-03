using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BE_Shop.Controllers
{
	public class OutputAddAddress : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		internal override void Query_DataInput(object? ip)
		{
			var json = JsonConvert.SerializeObject(ip);
			(string Address, Guid UserId) input = JsonConvert.DeserializeObject<(string, Guid)>(json);
			using (var db = new DatabaseConnection())
			{
				var i = db._User.Select(e => new { e.Id }).Where(e => e.Id == input.UserId).ToList().FirstOrDefault() ?? throw new HttpException(string.Empty, 401);
				db._Address.Add(new Address()
				{
					Id = Id,
					UserId = input.UserId,
					Description = input.Address,
				});
				db.SaveChanges();
			}
		}
	}
}