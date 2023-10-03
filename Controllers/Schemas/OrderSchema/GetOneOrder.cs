using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace BE_Shop.Controllers
{
	public class OutputGetOneOrder : Output
	{
		public Guid Id { get; set; } = Guid.Empty;
		public object User { get; set; }
		public string Address { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; } = DateTime.MinValue;
		public int Status { get; set; } = 0;
		public float Tax { get; set; } = 0.06F;
		public long TotalPrice { get; set; }
		public long TotalTax { get; set; }
		public object OrderDetail { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
				var order = db._Order.Join(db._OrderDetail, x => x.Id, y => y.OrderId, (x, y) => new
				{
					x.Id,
					x.Address,
					x.Status,
					x.UserId,
					x.CreatedDate,
					x.Tax,
					y.ItemCount,
					y.UnitPrice
				})
				.Where(e => e.Id == Id)
				.OrderByDescending(e => e.CreatedDate)
				.GroupBy(e => new { e.Id, e.Address, e.Status, e.CreatedDate, e.Tax, e.UserId})//e.OrderDetail, e.User})
				.Select(e => new { e.Key, TotalPrice = e.Sum(x => (x.ItemCount * x.UnitPrice)) , TotalTax = e.Key.Tax * e.Sum(x => (x.ItemCount * x.UnitPrice)) })
				.FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				this.Id = order.Key.Id;
				this.Address = order.Key.Address;
				this.Status = order.Key.Status;
				this.CreatedDate = order.Key.CreatedDate;
				this.Tax = order.Key.Tax;
				this.TotalPrice = order.TotalPrice;
				this.TotalTax = (long)order.TotalTax;
				this.User = db._User.Where(e => e.Id == order.Key.UserId).Select(i => new { i.Id, i.Name, i.Role }).FirstOrDefault();
				this.OrderDetail = db._OrderDetail
					.Where(e => e.OrderId == Id)
					.Select(e => new 
					{
						e.ProductId,
						ProductName = db._Product.Where(x => x.Id == e.ProductId).Select(x => x.Name).FirstOrDefault(),
						e.UnitPrice,
						e.ItemCount,
						TotalPrice = e.UnitPrice * e.ItemCount 
					})
					.ToList();
			}
		}
	}
}