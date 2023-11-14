using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace BE_Shop.Controllers
{
	public class OutputGetOneOrderData1
	{
		public Guid Id { get; set; }
		public string Address { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string MethodPayment { get; set; }
		public string Status { get; set; }
		public OutputGetOneOrderData2? User { get; set; }
		public long TotalPrice { get; set; }
		public List<OutputGetOneOrderData3> Detail { get; set; }
	}
	public class OutputGetOneOrderData2
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		public string Email { get; set; }
	}
	public class OutputGetOneOrderData3
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public long UnitPrice { get; set; }
		public long ItemCount { get; set; }
		public long TotalPrice { get; set; }
	}
	public class OutputGetOneOrder : Output
	{
		public OutputGetOneOrderData1 Order { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
                Order = db._Order
					.OrderByDescending(e => e.CreatedDate)
					.Where(e => e.Id == Id)
					.Select(e => new OutputGetOneOrderData1
					{
						Id = e.Id,
						Address = e.Address,
						CreatedDate = e.CreatedDate,
						MethodPayment = e.MethodPayment ? "Online" : "Offline",
						Status =	(e.Status == 0) ? "Khởi tạo" :
									(e.Status == 1) ? "Xác nhận" :
									(e.Status == 2) ? "Thanh toán" :
									(e.Status == 3) ? "Hoàn tất" :
									(e.Status == 4) ? "Hủy" : e.Status.ToString(),
						User = db._User.Where(y => y.Id == e.UserId).Select(y => new OutputGetOneOrderData2
						{
							Id = e.UserId,
                        	Name = y.Name,
                        	Role = y.Role,
							Email = y.UserName
						}).FirstOrDefault(),
						TotalPrice = db._OrderDetail
							.Where(y => y.OrderId == e.Id)
							.Select(y => y.UnitPrice * y.ItemCount)
							.Sum(),
						Detail = db._OrderDetail
							.Where(y => y.OrderId == Id)
							.Select(y => new OutputGetOneOrderData3
							{
								ProductId = y.ProductId,
								ProductName = db._Product.Find(y.ProductId) == null ? "" : db._Product.Find(y.ProductId).Name,
								UnitPrice = y.UnitPrice,
								ItemCount = y.ItemCount,
								TotalPrice = y.UnitPrice * y.ItemCount,
							})
							.ToList(),
                    })
					.FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}