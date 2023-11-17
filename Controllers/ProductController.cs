using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace BE_Shop.Controllers
{
	[Route("/api/product")]
	public class ProductController : BaseController
	{
		/// <summary>
		/// Thêm sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddProduct), 200)]
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
		[ProducesResponseType(typeof(OutputUpdateProduct), 200)]
		public async Task<IActionResult> Update([FromBody] UpdateProduct input)
		{
			return await QueryCheck<OutputUpdateProduct>(input);
		}
        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
		[HttpDelete("{Id}")]
		[ProducesResponseType(typeof(OutputDeleteProduct), 200)]
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
		[ProducesResponseType(typeof(OutputGetAllProduct), 200)]
		public async Task<IActionResult> GetAll([FromBody] GetAllProduct input)
		{
			return await QueryCheck_NonToken<OutputGetAllProduct>(input);
		}
        /// <summary>
        /// Lấy danh sách sản phẩm dành cho admin
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("la")]
		[ProducesResponseType(typeof(OutputGetAllAdminProduct), 200)]
        public async Task<IActionResult> GetAllAdmin([FromBody] GetAllProduct input)
        {
            return await QueryCheck<OutputGetAllAdminProduct>(input);
        }
        /// <summary>
        /// Lấy thông tin sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AllowAnonymous]
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(OutputGetOneProduct), 200)]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck_NonToken<OutputGetOneProduct>(Id);
		}
	}
}
