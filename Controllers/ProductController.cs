using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_Shop.Controllers
{
	[ApiController]
	[Route("/api/product")]
	[Produces("application/json")]
	public class ProductController : ControllerBase
	{
		/// <summary>
		/// Thêm sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public async Task<Output_base<OutputAddProduct>> Add([FromBody] AddProduct input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputAddProduct>(input);
			});
		}
		/// <summary>
		/// Sửa sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<Output_base<OutputUpdateProduct>> Update([FromBody] UpdateProduct input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputUpdateProduct>(input);
			});
		}
		/// <summary>
		/// Xóa sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{Id}")]
		public async Task<Output_base<OutputDeleteProduct>> Delete(string input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputDeleteProduct>(input);
			});
		}
		/// <summary>
		/// Lấy danh sách sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public async Task<Output_base<OutputGetAllProduct>> GetAll([FromBody] GetAllProduct input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetAllProduct>(input);
			});
		}
		/// <summary>
		/// Lấy thông tin sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize]
		[HttpGet("{Id}")]
		public async Task<Output_base<OutputGetOneProduct>> GetOne(string input)
		{
			return await Task.Run(() =>
			{
				return new Output_base<OutputGetOneProduct>(input);
			});
		}
	}
}
