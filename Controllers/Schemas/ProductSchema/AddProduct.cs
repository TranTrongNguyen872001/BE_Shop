using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class AddProduct
	{
		public string Name { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
		public int Rating { get; set; } = 0;
		public long UnitPrice { get; set; } = 0;
		public int TotalItem { get; set; } = 0;
	}
	public class OutputAddProduct : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		internal override void Query_DataInput(object? ip)
		{
			AddProduct input = (AddProduct)ip;
			using (var db = new DatabaseConnection())
			{
				db._Product.Add(new Product()
				{
					Id = Id,
					Name = input.Name,
					Decription = input.Decription,
					UnitPrice = input.UnitPrice,
					TotalItem = input.TotalItem,
					Rating = input.Rating,
				});
				db.SaveChanges();
			}
		}
	}
}