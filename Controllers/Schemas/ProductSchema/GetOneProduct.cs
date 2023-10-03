using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetOneProduct : Output
	{
		public Product Product { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				Product = db._Product.Where(e => e.Id == id).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}