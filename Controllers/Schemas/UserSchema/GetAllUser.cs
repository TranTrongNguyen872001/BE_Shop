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
	public class OutputGetAllUser_User
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
	}

	public class OutputGetAllUser : Output
    {
        public object Users { get; set; }
        public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
        {
			GetAllUser getAllUser = (GetAllUser)ip;
			using (var db = new DatabaseConnection())
			{
				Users = getAllUser.Desc ?
						db._User.Select(e => new { e.Id, e.Name, e.UserName, e.Role })
						.OrderBy(e => EF.Property<object>(e, getAllUser.SortBy ?? "Name"))
						.Where(e => getAllUser.Search == string.Empty
							|| getAllUser.Search.Contains(e.Name)
							|| getAllUser.Search.Contains(e.Role)
							|| getAllUser.Search.Contains(e.UserName))
						.Skip((getAllUser.Page - 1) * getAllUser.Index)
						.Take(getAllUser.Index)
						.ToList() :
						db._User.Select(e => new { e.Id, e.Name, e.UserName, e.Role })
						.OrderByDescending(e => EF.Property<object>(e, getAllUser.SortBy ?? "Name"))
						.Where(e => getAllUser.Search == string.Empty
							|| getAllUser.Search.Contains(e.Name)
							|| getAllUser.Search.Contains(e.Role)
							|| getAllUser.Search.Contains(e.UserName))
						.Skip((getAllUser.Page - 1) * getAllUser.Index)
						.Take(getAllUser.Index)
						.ToList();
				TotalItemCount = db._User
					.Where(e => getAllUser.Search.Contains(e.Name)
						|| getAllUser.Search.Contains(e.Role)
						|| getAllUser.Search.Contains(e.UserName)).Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)getAllUser.Index);
			}
		}
    }
}