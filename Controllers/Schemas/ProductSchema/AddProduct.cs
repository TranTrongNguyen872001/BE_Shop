using BE_Shop.Data;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Controllers
{
	public class AddProduct
	{
		public string Name { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
		public long UnitPrice { get; set; } = 0;
		public int TotalItem { get; set; } = 0;
	}
	public class OutputAddProduct : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			AddProduct input = (AddProduct)ip;

		}
	}
}