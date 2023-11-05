using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetOneProduct : Output
	{
        public object Product { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
                //var Product = db._Product.Find(id) ?? throw new HttpException(string.Empty, 404);
                Product = db._Product
					.Where(e => e.Id == id)
					.Select(e => new
					{
						e.Id,
						e.Name,
						e.Decription,
						Rating = Math.Round((db._Comment.Where(y => y.ProductId == e.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2,
						e.UnitPrice,
						e.TotalItem,
						Category = db._ProductCategory
							.Where(y => e.Category != null && e.Category.Contains(y.Id.ToString()))
							.ToList(),
						Files = db._FileManager
							.Where(f => f.OwnerId == id)
							.Select(e => new Guid(e.Id.ToString()))
							.ToList(),
                        Status = e.Active ? "Active" : "Unactive",
                    }).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}