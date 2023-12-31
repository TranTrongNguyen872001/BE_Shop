﻿using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[Route("/api/comment")]
	public class CommentController : BaseController
	{
		/// <summary>
		/// Thêm phản hồi
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddComment), 200)]
		public async Task<IActionResult> Add([FromBody] AddComment input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Name).Value ?? throw new HttpException(string.Empty, 401));
			return await QueryCheck<OutputAddComment>(input);
		}
		/// <summary>
		/// Lấy danh sách phản hồi
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("list")]
		[ProducesResponseType(typeof(OutputGetAllComment), 200)]
		public async Task<IActionResult> GetAll([FromBody] GetAllComment input)
		{
			return await QueryCheck_NonToken<OutputGetAllComment>(input);
		}
	}
}
