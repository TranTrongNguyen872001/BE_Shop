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
	public class OutputGetAllOrder : Output
	{
		public object OrderList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			GetAllOrder input = (GetAllOrder)ip;
			using (var db = new DatabaseConnection())
			{
				OrderList = db._Order.Join(db._OrderDetail, x => x.Id, y => y.OrderId, (x, y) => new
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
				.OrderByDescending(e => e.CreatedDate)
				.GroupBy(e => new { e.Id, e.Address, e.User, e.Status, e.CreatedDate })
				.Select(e => new { e.Key, TotalPrice = e.Sum(x => (x.ItemCount * x.UnitPrice)) })
				.Skip((input.Page - 1) * input.Index)
				.Take(input.Index)
				.ToList();
				TotalItemCount = db._Order.Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
	public class OutputGetAllOrderMine : Output
	{
		public object OrderList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			GetAllOrder input = (GetAllOrder)ip;
			using (var db = new DatabaseConnection())
			{
				OrderList = db._Order.Join(db._OrderDetail, x => x.Id, y => y.OrderId, (x, y) => new
				{
					x.Id,
					x.Address,
					x.UserId,
                    Status =	(x.Status == 0) ? "Khởi tạo" :
                                (x.Status == 1) ? "Xác nhận" :
                                (x.Status == 2) ? "Thanh toán" :
                                (x.Status == 3) ? "Hoàn tất" :
                                (x.Status == 4) ? "Hủy" : x.Status.ToString(),
					x.CreatedDate,
					y.ItemCount,
					y.UnitPrice
				})
				.Where(e => e.UserId == input.UserId)
				.OrderByDescending(e => e.CreatedDate)
				.GroupBy(e => new { e.Id, e.Address, e.Status, e.CreatedDate})
				.Select(e => new { e.Key, TotalPrice = e.Sum(x => (x.ItemCount * x.UnitPrice)) })
				.Skip((input.Page - 1) * input.Index)
				.Take(input.Index)
				.ToList();
				TotalItemCount = db._Order.Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
}