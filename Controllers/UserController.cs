﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BE_Shop.Data;
using System.Security.Claims;
using BE_Shop.Data.Service;

namespace BE_Shop.Controllers
{
	/// <summary>
	/// Đăng ký / đăng nhập
	/// </summary>
	[Route("/api/user")]
	public class UserController : BaseController
	{
		static internal string key = "MisaProject1412NguyenTran872001Kaitokids";
		//private readonly IEmailService emailService;

		// public UserController(IEmailService emailService)
		// {
		// 	this.emailService = emailService;
		// }
		/// <summary>
		/// Đăng ký
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddUser), 200)]
		public async Task<IActionResult> AddUser([FromBody] AddUser input)
		{
			return await QueryCheck_NonToken<OutputAddUser>(input);
		}
		/// <summary>
		/// Đăng nhập
		/// </summary>
		/// <param name="input"></param>
		/// <returns>một Token có thời hạn, sử dụng để gọi các giao thức khác</returns>
		[AllowAnonymous]
		[HttpPost("lg")]
		[ProducesResponseType(typeof(OutputLogin), 200)]
		public async Task<IActionResult> Login([FromBody] Login input)
		{
			return await QueryCheck_NonToken<OutputLogin>(input);
		}
		/// <summary>
		/// Danh sách tài khoản
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("list")]
		[ProducesResponseType(typeof(OutputGetAllUser), 200)]
		public async Task<IActionResult> GetAllUser([FromBody] GetAllUser input)
		{
			return await QueryCheck<OutputGetAllUser>(input);
		}
		/// <summary>
		/// Upload ảnh đại diện
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[DisableRequestSizeLimit]
		[HttpPost("pro/pic")]
		public async Task<IActionResult> AddProfilePicture(IFormFile file)
		{
			try
			{
				using (var db = new DatabaseConnection())
				{
					var user = db._User.Find(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401)));
					using (var ms = new MemoryStream())
					{
						file.CopyTo(ms);
						user.ProPic = ms.ToArray();
						user.ProPicType = file.ContentType;
						db.SaveChanges();
					}
					return Ok();
				}
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
		}
		/// <summary>
		/// Đổi thông tin tài khoản
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPut]
		[ProducesResponseType(typeof(OutputUpdateUser), 200)]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUser input)
		{
			input.Id = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
			return await QueryCheck<OutputUpdateUser>(input);
		}
		/// <summary>
		/// Đổi thông tin tài khoản cho admin
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPut("admin")]
		[ProducesResponseType(typeof(OutputUpdateUserForadmin), 200)]
		public async Task<IActionResult> UpdateUserForAdmin([FromBody] UpdateUserForadmin input)
		{
			return await QueryCheck<OutputUpdateUserForadmin>(input);
		}
		/// <summary>
		/// Xác thực tài khoản
		/// </summary>
		/// <param name="ValidCode"></param>
		/// <returns></returns>
		// [Authorize(Roles = "NotValid")]
		// [HttpPut("{ValidCode}")]
		// [ProducesResponseType(typeof(OutputValidUser), 200)]
		// public async Task<IActionResult> ValidUser(string ValidCode)
		// {
		// 	return await QueryCheck<OutputValidUser>(new ValidUser()
		// 	{
		// 		UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401)),
		// 		ValidCode = int.Parse(ValidCode),
		// 		EmailService = this.emailService
		// 	});
		// }
		/// <summary>
		/// Gửi mã xác thực tài khoản
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		// [Authorize(Roles = "NotValid")]
		// [HttpGet]
		// [ProducesResponseType(typeof(OutputSendValidCode), 200)]
		// public async Task<IActionResult> SendValidCode()
		// {
		// 	return await QueryCheck<OutputSendValidCode>(new SendValidCode()
		// 	{
		// 		UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401)),
		// 		EmailService = this.emailService
		// 	});
		// }
		/// <summary>
		/// Lấy thông tin tài khoản theo ID
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(OutputGetOneUser), 200)]
		public async Task<IActionResult> UserOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneUser>(Id);
		}
		/// <summary>
		/// Lấy thông tin tài khoản theo token
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpGet("pro")]
		[ProducesResponseType(typeof(OutputGetOneUser), 200)]
		public async Task<IActionResult> GetProfile()
		{
			return await QueryCheck<OutputGetOneUser>(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401)));
		}
		/// <summary>
		/// Lấy ảnh đại diện theo Id
		/// </summary>
		[AllowAnonymous]
		[HttpGet("pro/pic/{Id}")]
		public IActionResult GetProfilePicture(Guid Id)
		{
			try
			{
				using (var db = new DatabaseConnection())
				{
					var user = db._User.Find(Id) ?? throw new HttpException(string.Empty, 404);
					if(user.ProPic != null){
						return File(user.ProPic, user.ProPicType ?? "image/jpg");
					}
					else{
						var file = db._FileManager.Find(Guid.Parse("00000000-0000-0000-0000-000000000000")) ?? throw new HttpException(string.Empty, 404);
						return File(file.Source, file.Type);
					}
				}
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Lấy ảnh đại diện
		/// </summary>
		[AllowAnonymous]
		[HttpGet("pro/pic")]
		public IActionResult GetProfilePicture()
		{
			try
			{
				using (var db = new DatabaseConnection())
				{
					var user = db._User.Find(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401))) ?? throw new HttpException(string.Empty, 404);
					if(user.ProPic != null){
						return File(user.ProPic, user.ProPicType ?? "image/jpg");
					}
					else{
						var file = db._FileManager.Find(Guid.Parse("00000000-0000-0000-0000-000000000000")) ?? throw new HttpException(string.Empty, 404);
						return File(file.Source, file.Type);
					}
				}
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// Xóa tài khoản
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpDelete("{Id}")]
		[ProducesResponseType(typeof(OutputDeleteUser), 200)]
		public async Task<IActionResult> DeleteUser(Guid Id)
		{
			return await QueryCheck<OutputDeleteUser>(Id);
		}
	}
}