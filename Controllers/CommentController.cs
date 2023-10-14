using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[ApiController]
	[Route("/api/comment")]
	[Produces("application/json")]
	public class CommentController : BaseController
	{
		/// <summary>
		/// Thêm phản hồi
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddComment input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Name).Value);
			return await QueryCheck<OutputAddComment>(input);
		}
		/// <summary>
		/// Lấy danh sách phản hồi
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("list")]
		public async Task<IActionResult> GetAll([FromBody] GetAllComment input)
		{
			return await QueryCheck<OutputGetAllComment>(input);
		}
	}
}
