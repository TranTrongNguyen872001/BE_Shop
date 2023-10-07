using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BE_Shop.Controllers
{
	public class AddAddress
	{
		internal string Address { get; set; } = string.Empty;
		internal Guid UserId { get; set; } = Guid.Empty;
	}
	public class OutputAddAddress : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		internal override void Query_DataInput(object? ip)
		{
			AddAddress input = (AddAddress)ip;
			using (var db = new DatabaseConnection())
			{
				var i = db._User.Find(input.UserId) ?? throw new HttpException(string.Empty, 401);
				if(input.Address == string.Empty) { throw new HttpException(string.Empty, 400); }
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