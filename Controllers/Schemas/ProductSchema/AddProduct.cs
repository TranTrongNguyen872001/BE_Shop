using BE_Shop.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;

namespace BE_Shop.Controllers
{
	public class AddProduct
	{
		public string Name { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public float Discount { get; set; } = 0;
		public string Decription { get; set; } = string.Empty;
		public long UnitPrice { get; set; } = 0;
		public int TotalItem { get; set; } = 0;
		public Guid MainFile { get; set; } = Guid.Empty;
		public List<Guid> files {  get; set; } = new List<Guid>();
	}
	public class OutputAddProduct : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		internal override void Query_DataInput(object? ip)
		{
			AddProduct input = (AddProduct)ip;
			using (var db = new DatabaseConnection())
			{
				foreach(var file in input.files)
				{
					var f = db._FileManager.Find(file);
					if (f != null)
					{
						f.OwnerId = this.Id;
					}
				}
				db._Product.Add(new Product()
				{
					Id = this.Id,
					Name = input.Name,
					Decription = input.Decription,
					UnitPrice = input.UnitPrice,
					TotalItem = input.TotalItem,
					MainFile = input.MainFile,
					Code = input.Code,
					Discount = input.Discount,
				});
				db.SaveChanges();
			}
		}
	}
}