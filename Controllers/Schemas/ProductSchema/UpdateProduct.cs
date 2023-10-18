using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class UpdateProduct
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
		public long UnitPrice { get; set; } = 0;
		public int TotalItem { get; set; } = 0;
		public Guid MainFile { get; set; } = Guid.Empty;
		public float Discount { get; set; } = 0;
		public List<Guid> files { get; set; } = new List<Guid>();
	}
	public class OutputUpdateProduct : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateProduct input = (UpdateProduct)ip;
			using (var db = new DatabaseConnection())
			{
				var Product = db._Product.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
				foreach (var file in input.files)
				{
					var f = db._FileManager.Find(file);
					if (f != null)
					{
						f.OwnerId = Product.Id;
					}
				}
				Product.Name = input.Name;
				Product.Decription = input.Decription;
				Product.UnitPrice = input.UnitPrice;
				Product.TotalItem = input.TotalItem;
				Product.MainFile = input.MainFile;
				Product.Discount = input.Discount;
				db.SaveChanges();
			}
		}
	}
}