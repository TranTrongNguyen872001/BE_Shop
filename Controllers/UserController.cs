using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BE_Shop.Data;
using BE_Shop.Controllers;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
    /// <summary>
    /// Đăng ký / đăng nhập
    /// </summary>
    [ApiController]
    [Route("/api/user")]
	[Produces("application/json")]
	public class UserController : BaseController
    {
		static internal string key = "MisaProject1412NguyenTran872001Kaitokids";
		/// <summary>
		/// Đăng nhập
		/// </summary>
		/// <param name="input"></param>
		/// <returns>một Token có thời hạn, sử dụng để gọi các giao thức khác</returns>
		[AllowAnonymous]
		[HttpPost("lg")]
		public async Task<IActionResult> Login([FromBody] Login input)
		{
			return await QueryCheck<OutputLogin>(input);
		}
		/// <summary>
		/// Đăng ký
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> AddUser([FromBody] AddUser input)
		{
			return await QueryCheck<OutputAddUser>(input);
		}
		/// <summary>
		/// Đổi thông tin tài khoản
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUser input)
		{
			return await QueryCheck<OutputUpdateUser>(input);
		}
		/// <summary>
		/// Danh sách tài khoản
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("list")]
		public async Task<IActionResult> GetAllUser([FromBody] GetAllUser input)
		{
			return await QueryCheck<OutputGetAllUser>(input);
		}
		/// <summary>
		/// Lấy thông tin tài khoản theo ID
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpGet("{Id}")]
		public async Task<IActionResult> UserOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneUser>(Id);
		}
		/// <summary>
		/// Lấy thông tin tài khoản theo token
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet("pro")]
		public async Task<IActionResult> GetProfile()
		{
			return await QueryCheck<OutputGetOneUser>(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value));
		}
		/// <summary>
		/// Xóa tài khoản
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> DeleteUser(Guid Id)
		{
			return await QueryCheck<OutputDeleteUser>(Id);
		}
	}
}