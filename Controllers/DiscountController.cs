using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[Route("/api/dis")]
	public class DiscountController : BaseController
	{
        /// <summary>
        /// Thêm mã giảm giá
        /// </summary>
        /// <param name="Discount"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddDiscount), 200)]
		public async Task<IActionResult> Add([FromBody] AddDiscount Discount)
		{
            return await QueryCheck<OutputAddDiscount>(Discount);
		}
        /// <summary>
        /// Xóa mã giảm giá
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
		[HttpDelete("{Id}")]
		[ProducesResponseType(typeof(OutputDeleteDiscount), 200)]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteDiscount>(Id);
		}
		/// <summary>
		/// Lấy danh sách mã giảm giá
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost("list")]
		[ProducesResponseType(typeof(OutputGetAllDiscount), 200)]
		public async Task<IActionResult> GetAll()
		{
			return await QueryCheck<OutputGetAllDiscount>(null);
		}
		/// <summary>
		/// Lấy danh sách mã giảm giá dành cho damin
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost("la")]
		[ProducesResponseType(typeof(OutputGetAllDiscountAdmin), 200)]
		public async Task<IActionResult> GetAllAdmin()
		{
			return await QueryCheck<OutputGetAllDiscountAdmin>(null);
		}
        /// <summary>
        /// Lấy thông tin mã giảm giá
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(OutputGetOneDiscount), 200)]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneDiscount>(Id);
		}
	}
}