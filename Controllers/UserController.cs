using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BE_Shop.Data;
using BE_Shop.Controllers;

namespace BE_Shop.Controllers
{
    /// <summary>
    /// Đăng ký / đăng nhập
    /// </summary>
    [ApiController]
    [Route("/api/user")]
	[Produces("application/json")]
	public class UserController : ControllerBase
    {
		static internal string key = "Misa_Project_1412";
		/// <summary>
		/// Đăng nhập
		/// </summary>
		/// <param name="input"></param>
		/// <returns>một Token có thời hạn, sử dụng để gọi các giao thức khác</returns>
		[AllowAnonymous]
		[HttpPost("lg")]
		public async Task<Output_base<Output_Login>> GetToken([FromBody] Login input)
		{
			return await Task.Run(()=>
			{
				return new Output_base<Output_Login>(input);
			});
		}
		/// <summary>
		/// Đăng ký
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<Output_base<OutputAddUser>> NewUser([FromBody] AddUser input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputAddUser>(input);
			});
		}
		/// <summary>
		/// Đổi mật khẩu
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<Output_base<OutputUpdateUser>> ResetPassword([FromBody] UpdateUser input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputUpdateUser>(input);
			});
		}
		/// <summary>
		/// Lấy lại token
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut("tk")]
		public async Task<Output_base<Output_ResetToken>> ResetToken([FromBody] ResetToken input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<Output_ResetToken>(input);
			});
		}
		/// <summary>
		/// Danh sách tài khoản
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<Output_base<OutputGetAllUser>> ListUser([FromBody] GetAllUser input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetAllUser>(input);
			});
		}
		/// <summary>
		/// Lấy thông tin tài khoản theo ID
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet("{Id}")]
		public async Task<Output_base<OutputGetOneUser>> UserById(string Id)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetOneUser>(Id);
			});
		}
		/// <summary>
		/// Xóa tài khoản
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<Output_base<Output_DeleteUser>> DeleteUser(string Id)
		{
			return await Task.Run(() =>
			{
				return new Output_base<Output_DeleteUser>(Id);
			});
		}
	}
}