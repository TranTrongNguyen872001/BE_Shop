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
		public object Data { get; set; }
		public object OrderDetail { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip;
			using (var db = new DatabaseConnection())
			{
                Data = db._Order.Join(db._OrderDetail, x => x.Id, y => y.OrderId, (x, y) => new
				{
					x.Id,
					x.Address,
                    User = new
                    {
                        Id = x.UserId,
                        Name = db._User.Where(e => e.Id == x.UserId).Select(i => i.Name).FirstOrDefault(),
                        Role = db._User.Where(e => e.Id == x.UserId).Select(i => i.Role).FirstOrDefault()
                    },
                    Status =	(x.Status == 0) ? "Khởi tạo" :
                                (x.Status == 1) ? "Xác nhận" :
                                (x.Status == 2) ? "Thanh toán" :
                                (x.Status == 3) ? "Hoàn tất" :
                                (x.Status == 4) ? "Hủy" : x.Status.ToString(),
                    x.CreatedDate,
					y.ItemCount,
					y.UnitPrice,
				})
				.Where(e => e.Id == Id)
				.OrderByDescending(e => e.CreatedDate)
				.GroupBy(e => new { e.Id, e.Address, e.Status, e.CreatedDate, e.User})//e.OrderDetail, e.User})
				.Select(e => new { e.Key, TotalPrice = e.Sum(x => (x.ItemCount * x.UnitPrice)) })
				.FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				this.OrderDetail = db._OrderDetail
				.Where(e => e.OrderId == Id)
				.Select(e => new
				{
					e.ProductId,
					ProductName = db._Product.Where(x => x.Id == e.ProductId).Select(x => x.Name).FirstOrDefault(),
					e.UnitPrice,
					e.ItemCount,
					TotalPrice = e.UnitPrice * e.ItemCount,
				})
				.ToList();
			}
		}
	}
}