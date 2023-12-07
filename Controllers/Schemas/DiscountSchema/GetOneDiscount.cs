using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetOneDiscount : Output
	{
		public Discount Discount { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip!;
			using(var db = new DatabaseConnection())
			{
				Discount = db._Discount.Find(Id) ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}