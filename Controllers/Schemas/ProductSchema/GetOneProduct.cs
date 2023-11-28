using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetOneProductData
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Decription { get; set; }
		public double Rating { get; set; }
		public long UnitPrice { get; set; }
		public int TotalItem { get; set; }
		public int Discount {get; set;}
		public List<ProductCategory> Category { get; set; }
		public List<Guid> Files { get; set; }
		public string Status { get; set; }
	}
	public class OutputGetOneProduct : Output
	{
        public OutputGetOneProductData Product { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
                //var Product = db._Product.Find(id) ?? throw new HttpException(string.Empty, 404);
                Product = db._Product
					.Where(e => e.Id == id)
					.Select(e => new OutputGetOneProductData
					{
						Id = e.Id,
						Name = e.Name,
						Decription = e.Decription,
						Rating = Math.Round((db._Comment.Where(y => y.ProductId == e.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2,
						UnitPrice = e.UnitPrice,
						TotalItem = e.TotalItem,
						Discount = e.Discount,
						Category = db._ProductCategory
							.Where(y => e.Category != null && e.Category.Contains(y.Id.ToString()))
							.ToList(),
						Files = db._FileManager
							.Where(y => y.OwnerId == id)
							.Select(y => y.Id)
							.ToList(),
                        Status = e.Active ? "Active" : "Unactive",
                    }).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}