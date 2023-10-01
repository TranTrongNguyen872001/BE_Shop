using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class GetAllAddress
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
		/// Giảm dẩn? Mặc định = false
		/// </summary>
		public bool Desc { get; set; } = false;
		/// <summary>
		/// Nhập giá trị tìm kiếm
		/// </summary>
		public string Search { get; set; } = string.Empty;
	}
	public class OutputGetAllAddress : Output
	{
		public object AddressList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			//var json = JsonConvert.SerializeObject(ip);
			//(GetAllAddress, Guid UserId) i = JsonConvert.DeserializeObject<(GetAllAddress, Guid)>(json);
			GetAllAddress input = (GetAllAddress)ip;
			using (var db = new DatabaseConnection())
			{
				AddressList = input.Desc ?
						db._Address
						.OrderBy(e => e.Description)
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Description))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList() :
						db._Address
						.OrderByDescending(e => e.Description)
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Description))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList();
				TotalItemCount = db._Address
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Description)).Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
}