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
		public List<Product> ProductList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
		{
			GetAllProduct input = (GetAllProduct)ip;
			using (var db = new DatabaseConnection())
			{
				ProductList = input.Desc ?
						db._Product
						.OrderBy(e => EF.Property<object>(e, input.SortBy ?? "Name"))
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList() :
						db._Product
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