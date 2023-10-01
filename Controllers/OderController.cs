using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_Shop.Controllers
{
	/// <summary>
	/// Thông in địa chỉ
	/// </summary>
	[ApiController]
	[Route("/api/order")]
	[Produces("application/json")]
	public class OrderController : BaseController
	{
		/// <summary>
		/// Thêm hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddOrder input)
		{

			return await QueryCheck<OutputAddOrder>(input);
		}
		/// <summary>
		/// Sửa hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateOrder input)
		{
			return await QueryCheck<OutputUpdateOrder>(input);
		}
		/// <summary>
		/// Xóa hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(string Id)
		{
			return await QueryCheck<OutputDeleteOrder>(Id);
		}
		/// <summary>
		/// Lấy danh sách hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetAll([FromBody] GetAllOrder input)
		{
			return await QueryCheck<OutputGetAllOrder>(input);
		}
		/// <summary>
		/// Lấy thông tin hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOne(string Id)
		{
			return await QueryCheck<OutputGetOneOrder>(Id);
		}
	}
}
