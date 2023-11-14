using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Runtime.CompilerServices;

namespace BE_Shop.Controllers
{
	public class GetAllOrder
	{
		/// <summary>
		/// Số item trên 1 trang
		/// </summary>
		public int Index { get; set; } = 0;
		/// <summary>
		/// Số trang
		/// </summary>
		public int Page { get; set; } = 0;
		internal Guid UserId { get; set; } = Guid.Empty;
	}
	public class OutputGetAllOrderData1
	{
		public Guid Id { get; set; }
		public string Address { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string MethodPayment { get; set; }
		public string Status { get; set; }
		public OutputGetAllOrderData2? User { get; set; }
		public long TotalPrice { get; set; }
	}
	public class OutputGetAllOrderData2
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		public string Email { get; set; }
	}
	public class OutputGetAllOrder : Output
	{
		public List<OutputGetAllOrderData1> OrderList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			GetAllOrder input = (GetAllOrder)ip!;
			using (var db = new DatabaseConnection())
			{
				OrderList = db._Order
                .OrderByDescending(e => e.CreatedDate)
                .Select(e => new OutputGetAllOrderData1{
                    Id = e.Id,
                    Address = e.Address,
                    CreatedDate = e.CreatedDate,
                    MethodPayment = e.MethodPayment ? "Online" : "Offline",
                    Status =	(e.Status == 0) ? "Khởi tạo" :
                                (e.Status == 1) ? "Xác nhận" :
                                (e.Status == 2) ? "Thanh toán" :
                                (e.Status == 3) ? "Hoàn tất" :
                                (e.Status == 4) ? "Hủy" : e.Status.ToString(),
                    User = db._User.Where(y => y.Id == e.UserId).Select(y => new OutputGetAllOrderData2
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
				})
				.Skip((input.Page - 1) * input.Index)
				.Take(input.Index)
				.ToList();
				TotalItemCount = db._Order.Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
	public class OutputGetAllOrderById : Output
	{
		public List<OutputGetAllOrderData1> OrderList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			GetAllOrder input = (GetAllOrder)ip!;
			using (var db = new DatabaseConnection())
			{
				OrderList = db._Order
                .OrderByDescending(e => e.CreatedDate)
				.Where(e => e.UserId == input.UserId)
                .Select(e => new OutputGetAllOrderData1{
                    Id = e.Id,
                    Address = e.Address,
                    CreatedDate = e.CreatedDate,
                    MethodPayment = e.MethodPayment ? "Online" : "Offline",
                    Status =	(e.Status == 0) ? "Khởi tạo" :
                                (e.Status == 1) ? "Xác nhận" :
                                (e.Status == 2) ? "Thanh toán" :
                                (e.Status == 3) ? "Hoàn tất" :
                                (e.Status == 4) ? "Hủy" : e.Status.ToString(),
                    User = db._User.Where(y => y.Id == e.UserId).Select(y => new OutputGetAllOrderData2
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
				})
				.Skip((input.Page - 1) * input.Index)
				.Take(input.Index)
				.ToList();
				TotalItemCount = db._Order.Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
}