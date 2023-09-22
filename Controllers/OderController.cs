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
	public class OrderController : ControllerBase
	{
		/// <summary>
		/// Thêm hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public async Task<Output_base<OutputAddOrder>> Add([FromBody] AddOrder input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputAddOrder>(input);
			});
		}
		/// <summary>
		/// Sửa hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<Output_base<OutputUpdateOrder>> Update([FromBody] UpdateOrder input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputUpdateOrder>(input);
			});
		}
		/// <summary>
		/// Xóa hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<Output_base<OutputDeleteOrder>> Delete(string input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputDeleteOrder>(input);
			});
		}
		/// <summary>
		/// Lấy danh sách hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<Output_base<OutputGetAllOrder>> GetAll([FromBody] GetAllOrder input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetAllOrder>(input);
			});
		}
		/// <summary>
		/// Lấy thông tin hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet("{Id}")]
		public async Task<Output_base<OutputGetOneOrder>> GetOne(string input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetOneOrder>(input);
			});
		}
	}
}
