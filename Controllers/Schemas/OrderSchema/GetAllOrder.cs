using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
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
					UserId = db._User.Where(e => e.Id == x.UserId).Select(i => i.Id).FirstOrDefault(),
					UserName = db._User.Where(e => e.Id == x.UserId).Select(i => i.Name).FirstOrDefault(),
					UserRole = db._User.Where(e => e.Id == x.UserId).Select(i => i.Role).FirstOrDefault(),
					x.Status,
					x.CreatedDate,
					x.Tax,
					y.ItemCount,
					y.UnitPrice
				})
				.OrderByDescending(e => e.CreatedDate)
				.GroupBy(e => new { e.Id, e.Address, e.UserId, e.UserName, e.UserRole, e.Status, e.CreatedDate, e.Tax })
				.Select(e => new { e.Key, TotalPrice = e.Sum(x => (x.ItemCount * x.UnitPrice)), TotalTax = e.Key.Tax * e.Sum(x => (x.ItemCount * x.UnitPrice)) })
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
					x.Status,
					x.CreatedDate,
					x.Tax,
					y.ItemCount,
					y.UnitPrice
				})
				.Where(e => e.UserId == input.UserId)
				.OrderByDescending(e => e.CreatedDate)
				.GroupBy(e => new { e.Id, e.Address, e.Status, e.CreatedDate, e.Tax })
				.Select(e => new { e.Key, TotalPrice = e.Sum(x => (x.ItemCount * x.UnitPrice)), TotalTax = e.Key.Tax * e.Sum(x => (x.ItemCount * x.UnitPrice)) })
				.Skip((input.Page - 1) * input.Index)
				.Take(input.Index)
				.ToList();
				TotalItemCount = db._Order.Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
}