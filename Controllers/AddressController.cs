using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IActionResult> Add([FromBody] AddAddress input)
		{
			return await QueryCheck<OutputAddAddress>(input);
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
			return await QueryCheck<OutputUpdateAddress>(input);
		}
		/// <summary>
		/// Xóa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(string Id)
		{
			return await QueryCheck<OutputDeleteAddress>(Id);
		}
		/// <summary>
		/// Lấy danh sách địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
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
		public async Task<IActionResult> GetOne(string Id)
		{
			return await QueryCheck<OutputGetOneAddress>(Id);
		}
	}
}