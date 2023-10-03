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
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] string Address)
		{
			return await QueryCheck<OutputAddAddress>((Address, Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
		}
		/// <summary>
		/// Sửa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateAddress input)
		{
			return await QueryCheck<OutputUpdateAddress>((input, Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
		}
		/// <summary>
		/// Xóa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteAddress>((Id, Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
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
		[Authorize]
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneAddress>(Id);
		}
	}
}