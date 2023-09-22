using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_Shop.Controllers
{
	[ApiController]
	[Route("/api/address")]
	[Produces("application/json")]
	public class AddressController : ControllerBase
	{
		/// <summary>
		/// Thêm địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public async Task<Output_base<OutputAddAddress>> Add([FromBody] AddAddress input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputAddAddress>(input);
			});
		}
		/// <summary>
		/// Sửa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<Output_base<OutputUpdateAddress>> Update([FromBody] UpdateAddress input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputUpdateAddress>(input);
			});
		}
		/// <summary>
		/// Xóa địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<Output_base<OutputDeleteAddress>> Delete(string input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputDeleteAddress>(input);
			});
		}
		/// <summary>
		/// Lấy danh sách địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<Output_base<OutputGetAllAddress>> GetAll([FromBody] GetAllAddress input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetAllAddress>(input);
			});
		}
		/// <summary>
		/// Lấy thông tin địa chỉ
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet("{Id}")]
		public async Task<Output_base<OutputGetOneAddress>> GetOne(string input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetOneAddress>(input);
			});
		}
	}
}