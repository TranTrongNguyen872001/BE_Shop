using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[ApiController]
	[Route("/api/address")]
	[Produces("application/json")]
	public class AddressController : BaseController
	{
		/// <summary>
		/// Thêm địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] string Address)
		{
			return await QueryCheck<OutputAddAddress>(
				new AddAddress()
				{
					Address = Address, 
					UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)
				});
		}
		/// <summary>
		/// Sửa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateAddress input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
			return await QueryCheck<OutputUpdateAddress>(input);
		}
		/// <summary>
		/// Xóa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteAddress>(
				new DeleteAddress() 
				{
					UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value),
					AddressId = Id
				});
		}
		/// <summary>
		/// Lấy danh sách địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("list")]
		public async Task<IActionResult> GetAll([FromBody] GetAllAddress input)
		{
			return await QueryCheck<OutputGetAllAddress>(input);
		}
		/// <summary>
		/// Lấy thông tin địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneAddress>(Id);
		}
	}
}