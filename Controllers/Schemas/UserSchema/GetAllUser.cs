using BE_Shop.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_Shop.Controllers
{
    public class GetAllUser
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
	public class OutputGetAllUser : Output
    {
        public object UserList { get; set; }
        public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
        {
			GetAllUser input = (GetAllUser)ip;
			using (var db = new DatabaseConnection())
			{
				UserList = input.Desc ?
						db._User.Select(e => new { e.Id, e.Name, e.UserName, e.Role })
						.OrderBy(e => EF.Property<object>(e, input.SortBy ?? "Name"))
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name)
							|| input.Search.Contains(e.Role)
							|| input.Search.Contains(e.UserName))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList() :
						db._User.Select(e => new { e.Id, e.Name, e.UserName, e.Role })
						.OrderByDescending(e => EF.Property<object>(e, input.SortBy ?? "Name"))
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name)
							|| input.Search.Contains(e.Role)
							|| input.Search.Contains(e.UserName))
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.ToList();
				TotalItemCount = db._User
					.Where(e => input.Search.Contains(e.Name)
						|| input.Search.Contains(e.Role)
						|| input.Search.Contains(e.UserName)).Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
    }
}