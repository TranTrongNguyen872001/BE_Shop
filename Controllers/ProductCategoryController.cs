using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BE_Shop.Controllers
{
    [ApiController]
    [Route("/api/category")]
    [Produces("application/json")]
    public class ProductCategoryController : BaseController
    {
        /// <summary>
		/// Thêm danh mục danh mục sản phẩm
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddProductCategory input)
        {
            return await QueryCheck<OutputAddProductCategory>(input);
        }
        /// <summary>
        /// Sửa danh mục sản phẩm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCategory input)
        {
            return await QueryCheck<OutputUpdateProductCategory>(input);
        }
        /// <summary>
        /// Xóa danh mục sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return await QueryCheck<OutputDeleteProductCategory>(Id);
        }
        /// <summary>
        /// Lấy danh sách danh mục sản phẩm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("list")]
        public async Task<IActionResult> GetAll([FromBody] GetallProductCategory input)
        {
            return await QueryCheck<OutputGetallProductCategory>(input);
        }
        /// <summary>
        /// Lấy danh sách danh mục sản phẩm dành cho admin
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("la")]
        public async Task<IActionResult> GetAllAdmin([FromBody] GetallProductCategory input)
        {
            return await QueryCheck<OutputGetallProductCategory>(input);
        }
        /// <summary>
        /// Lấy thông tin danh mục sản phẩm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne(Guid Id)
        {
            return await QueryCheck<OutputGetoneProductCategory>(Id);
        }
    }
}
