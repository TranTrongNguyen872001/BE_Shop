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
					.Where(e => e.StopDate > DateTime.Now && e.Status == true)
					.ToList();
                TotalItemCount = db._Discount.Where(e => e.StopDate > DateTime.Now).Count();
			}
		}
	}
	public class OutputGetAllDiscountAdminData1 {
		public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime? StopDate { get; set; }
        public int Value {get; set;}
        public bool Status { get; set;}
		public int TotalOrder { get; set; }
	}
	public class OutputGetAllDiscountAdmin : Output
	{
		public List<OutputGetAllDiscountAdminData1> DiscountList { get; set; }
		public int TotalItemCount { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			using (var db = new DatabaseConnection())
			{
				DiscountList = db._Discount
                    .OrderByDescending(e => e.StopDate)
					.Select(e => new OutputGetAllDiscountAdminData1{
						Id = e.Id,
						Code = e.Code,
						Status = e.Status,
						StopDate = e.StopDate,
						Value = e.Value,
						TotalOrder = db._Order.Where(y => y.DiscountId == e.Id).Count(),
					})
					.ToList();
                TotalItemCount = db._Discount.Where(e => e.StopDate > DateTime.Now).Count();
			}
		}
	}
}