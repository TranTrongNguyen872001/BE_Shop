using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class OutputGetOneProduct : Output
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
		public double Rating { get; set; } = 0;
		public long UnitPrice { get; set; } = 0;
		public int TotalItem { get; set; } = 0;
		public List<Guid> Files { get; set; } = new List<Guid>();
		internal override void Query_DataInput(object? ip)
		{
			Guid id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				var Product = db._Product.Find(id) ?? throw new HttpException(string.Empty, 404);
				this.Id = Product.Id;
				this.Name = Product.Name;
				this.Decription = Product.Decription;
				this.Rating = Math.Round((db._Comment.Where(y => y.ProductId == Product.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2;
				this.UnitPrice = Product.UnitPrice;
				this.TotalItem = Product.TotalItem;
				this.Files = db._FileManager
					.Where(f => f.OwnerId == id)
					.Select(e => new Guid(e.Id.ToString()))
					.ToList();
			}
		}
	}
}