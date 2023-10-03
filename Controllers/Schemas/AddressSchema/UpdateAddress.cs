using BE_Shop.Data;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class UpdateAddress
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Decription { get; set; } = string.Empty;
	}
	public class OutputUpdateAddress : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			var json = JsonConvert.SerializeObject(ip);
			(UpdateAddress Address, Guid UserId) i = JsonConvert.DeserializeObject<(UpdateAddress, Guid)>(json);

			UpdateAddress input = i.Address;
			using (var db = new DatabaseConnection())
			{
				var Address = db._Address.Where(e => e.Id == input.Id).ToList().FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				if (Address.UserId != i.UserId)
				{
					throw new HttpException(string.Empty, 403);
				}
				if (input.Decription != string.Empty)
				{
					Address.Description = input.Decription;
					db.SaveChanges();
				}
			}
		}
	}
}