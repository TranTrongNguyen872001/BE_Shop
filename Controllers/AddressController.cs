using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[Route("/api/address")]
	public class AddressController : BaseController
	{
        /// <summary>
        /// Thêm địa chỉ
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddAddress), 200)]
		public async Task<IActionResult> Add([FromBody] AddAddress Address)
		{
			Address.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
            return await QueryCheck<OutputAddAddress>(Address);
		}
		/// <summary>
		/// Sửa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPut]
		[ProducesResponseType(typeof(OutputUpdateAddress), 200)]
		public async Task<IActionResult> Update([FromBody] UpdateAddress input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
			return await QueryCheck<OutputUpdateAddress>(input);
		}
        /// <summary>
        /// Xóa địa chỉ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpDelete("{Id}")]
		[ProducesResponseType(typeof(OutputDeleteAddress), 200)]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteAddress>(
				new DeleteAddress() 
				{
					UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401)),
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
		[ProducesResponseType(typeof(OutputGetAllAddress), 200)]
		public async Task<IActionResult> GetAll([FromBody] GetAllAddress input)
		{
			return await QueryCheck<OutputGetAllAddress>(input);
		}
        /// <summary>
        /// Lấy thông tin địa chỉ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(OutputGetOneAddress), 200)]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneAddress>(Id);
		}
	}
}