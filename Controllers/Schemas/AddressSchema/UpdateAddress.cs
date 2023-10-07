using BE_Shop.Data;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class UpdateAddress
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Decription { get; set; } = string.Empty;
		internal Guid UserId { get; set; } = Guid.Empty;
	}
	public class OutputUpdateAddress : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateAddress input = (UpdateAddress)ip;
			using (var db = new DatabaseConnection())
			{
				var Address = db._Address.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
				if (Address.UserId != input.UserId)
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