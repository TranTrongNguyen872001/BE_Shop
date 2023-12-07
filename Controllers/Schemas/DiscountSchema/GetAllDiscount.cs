using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class OutputGetAllDiscount : Output
	{
		public List<Discount> DiscountList { get; set; }
		public int TotalItemCount { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			using (var db = new DatabaseConnection())
			{
				DiscountList = db._Discount
                    .OrderByDescending(e => e.Value)
					.Where(e => e.StopDate > DateTime.Now)
					.ToList();
                TotalItemCount = db._Discount.Where(e => e.StopDate > DateTime.Now).Count();
			}
		}
	}
}