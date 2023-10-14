using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace BE_Shop.Controllers
{
	[ApiController]
	[Route("/api/product")]
	[Produces("application/json")]
	public class ProductController : BaseController
	{
		/// <summary>
		/// Thêm sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddProduct input)
		{
			return await QueryCheck<OutputAddProduct>(input);
		}
		/// <summary>
		/// Sửa sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProduct input)
		{
			return await QueryCheck<OutputUpdateProduct>(input);
		}
		/// <summary>
		/// Xóa sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteProduct>(Id);
		}
		/// <summary>
		/// Lấy danh sách sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("list")]
		public async Task<IActionResult> GetAll([FromBody] GetAllProduct input)
		{
			return await QueryCheck<OutputGetAllProduct>(input);
		}
		/// <summary>
		/// Lấy thông tin sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneProduct>(Id);
		}
	}
}
