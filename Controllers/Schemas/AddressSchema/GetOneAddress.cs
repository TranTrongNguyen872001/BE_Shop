using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetOneAddress : Output
	{
		public Address Address { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip;
			using(var db = new DatabaseConnection())
			{
				Address = db._Address.Find(Id) ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}