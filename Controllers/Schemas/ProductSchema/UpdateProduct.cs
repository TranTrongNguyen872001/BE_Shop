using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class UpdateProduct
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
		public int Rating { get; set; } = 0;
		public long UnitPrice { get; set; } = 0;
		public int TotalItem { get; set; } = 0;
	}
	public class OutputUpdateProduct : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateProduct input = (UpdateProduct)ip;
			using (var db = new DatabaseConnection())
			{
				var Product = db._Product.Where(x => x.Id == input.Id).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				Product.Name = input.Name;
				Product.Decription = input.Decription;
				Product.Rating = input.Rating;
				Product.UnitPrice = input.UnitPrice;
				Product.TotalItem = input.TotalItem;
				db.SaveChanges();
			}
		}
	}
}