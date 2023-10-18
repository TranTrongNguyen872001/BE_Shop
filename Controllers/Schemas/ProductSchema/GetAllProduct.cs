using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace BE_Shop.Controllers
{
	public class GetAllProduct
	{
		/// <summary>
		/// Số item trên 1 trang
		/// </summary>
		public int Index { get; set; } = 0;
		/// <summary>
		/// Số trang
		/// </summary>
		public int Page { get; set; } = 0;
		/// <summary>
		/// Sắp xếp theo
		/// </summary>
		public string? SortBy { get; set; }
		/// <summary>
		/// Giảm dẩn? Mặc định = false
		/// </summary>
		public bool Desc { get; set; } = false;
		/// <summary>
		/// Nhập giá trị tìm kiếm
		/// </summary>
		public string Search { get; set; } = string.Empty;
	}
	
	public class OutputGetAllProduct : Output
	{
		public object ProductList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			GetAllProduct input = (GetAllProduct)ip;
			using (var db = new DatabaseConnection())
			{
				ProductList = input.Desc ?
						db._Product
						.Select(e => new 
						{
							e.Code,
							e.Id,
							e.Name,
							MainFile = e.MainFile != Guid.Empty ? e.MainFile : db._FileManager.Where(y => y.OwnerId == e.Id).FirstOrDefault().Id,
							Rating = Math.Round((db._Comment.Where(y => y.ProductId == e.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2,
							e.UnitPrice,
							e.TotalItem,
						})
						.OrderBy(e => EF.Property<object>(e, input.SortBy ?? "Name"))
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList() :
						db._Product
						.Select(e => new
						{
							e.Code,
							e.Id,
							e.Name,
							MainFile = e.MainFile != Guid.Empty ? e.MainFile : db._FileManager.Where(y => y.OwnerId == e.Id).FirstOrDefault().Id,
							Rating = Math.Round((db._Comment.Where(y => y.ProductId == e.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2,
							e.UnitPrice,
							e.TotalItem,
						})
						.OrderByDescending(e => EF.Property<object>(e, input.SortBy ?? "Name"))
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList();
				TotalItemCount = db._Product
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name)).Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
}