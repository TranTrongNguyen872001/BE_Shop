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
		public object Order { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
                Order = db._Order
					.OrderByDescending(e => e.CreatedDate)
					.Where(e => e.Id == Id)
					.Select(e => new
					{
						e.Id,
						e.Address,
						e.CreatedDate,
						MethodPayment = e.MethodPayment ? "Online" : "Offline",
						Status =	(e.Status == 0) ? "Khởi tạo" :
									(e.Status == 1) ? "Xác nhận" :
									(e.Status == 2) ? "Thanh toán" :
									(e.Status == 3) ? "Hoàn tất" :
									(e.Status == 4) ? "Hủy" : e.Status.ToString(),
						User = new
						{
							Id = e.UserId,
							Name = db._User.Where(y => y.Id == e.UserId).Select(y => y.Name).FirstOrDefault(),
							Role = db._User.Where(y => y.Id == e.UserId).Select(y => y.Role).FirstOrDefault()
						},
						TotalPrice = db._OrderDetail
							.Where(y => y.OrderId == e.Id)
							.Select(y => y.UnitPrice * y.ItemCount)
							.Sum(),
						Detail = db._OrderDetail
							.Where(y => y.OrderId == Id)
							.Select(y => new
							{
								y.ProductId,
								ProductName = db._Product.Where(x => x.Id == y.ProductId).Select(x => x.Name).FirstOrDefault(),
								y.UnitPrice,
								y.ItemCount,
								TotalPrice = y.UnitPrice * y.ItemCount,
							})
							.ToList(),
                    })
					.FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}