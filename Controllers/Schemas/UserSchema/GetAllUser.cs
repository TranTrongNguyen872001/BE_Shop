﻿using BE_Shop.Data;
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
		public string? Role {get; set;} = null;
		public bool? Status{get; set;} = null;
    }
	public class OutputGetAllUserData
    {
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
		public bool? Gender { get; set; }
		public DateTime? Birthday { get; set; }
		public int TotalOrder { get; set; }
		public long TotalSpent { get; set; }
		public bool Status { get; set; }
	}
	public class OutputGetAllUser : Output
    {
        public List<OutputGetAllUserData> UserList { get; set; }
        public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }

		internal override void Query_DataInput(object? ip)
        {
			GetAllUser input = (GetAllUser)ip!;
			using (var db = new DatabaseConnection())
			{
				var temp = input.Desc ? db._User.OrderBy(e => EF.Property<object>(e, input.SortBy ?? "Name")) : db._User.OrderByDescending(e => EF.Property<object>(e, input.SortBy ?? "Name"));
				UserList = temp
					.Select(e => new OutputGetAllUserData
					{
						Id = e.Id,
						Name = e.Name,
						UserName = e.UserName,
						Role = e.Role,
						Status = e.Status,
						Gender = e.Gender,
						Birthday = e.Birthday,
						TotalOrder = db._Order.Where(y => y.UserId == e.Id).Count(),
                        TotalSpent = db._Order
							.Join(db._OrderDetail, x => x.Id, y => y.OrderId, 
								(x,y)=> new
								{
									x.UserId,
									y.ItemCount,
									y.UnitPrice
								})
							.Where(y => y.UserId == e.Id)
							.Select(y => y.ItemCount * y.UnitPrice)
							.Sum()
                    })
					.Where(e => (input.Search == string.Empty
							|| input.Search.Contains(e.Name ?? "")
							|| input.Search.Contains(e.Role)
							|| input.Search.Contains(e.UserName))
							&& (input.Role == null || input.Role == e.Role)
							&& (input.Status == null || input.Status == e.Status))
					.Skip((input.Page - 1) * input.Index)
					.Take(input.Index)
					.ToList();
                TotalItemCount = db._User
					.Where(e => (input.Search == string.Empty
							|| input.Search.Contains(e.Name ?? "")
							|| input.Search.Contains(e.Role)
							|| input.Search.Contains(e.UserName))
							&& (input.Role == null || input.Role == e.Role)
							&& (input.Status == null || input.Status == e.Status))
					.Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
    }
}