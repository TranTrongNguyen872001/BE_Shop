using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[Route("/api/file")]
	public class FileController : BaseController
	{
		/// <summary>
		/// Thêm File
		/// </summary>
		[Authorize(Roles = "Admin,Member")]
		[DisableRequestSizeLimit]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddFileManager), 200)]
		public async Task<IActionResult> Add([FromForm] List<IFormFile> files)
		{
			return await QueryCheck<OutputAddFileManager>(files);
		}
		/// <summary>
		/// Xóa file
		/// </summary>
		[Authorize(Roles = "Admin,Member")]
		[HttpDelete("{Id}")]
		[ProducesResponseType(typeof(OutputDeleteFileManager), 200)]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteFileManager>(Id);
		}
		/// <summary>
		/// Lấy danh sách tất cả file đã lưu (chưa rõ mục đích sử dụng)
		/// </summary>
		[Authorize(Roles = "Admin")]
		[HttpGet]
		[ProducesResponseType(typeof(OutputGetAllFileManager), 200)]
		public async Task<IActionResult> GetAll()
		{
			return await QueryCheck<OutputGetAllFileManager>(null);
		}
		/// <summary>
		/// Lấy file
		/// </summary>
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			try
			{
				using (var db = new DatabaseConnection())
				{
					var file = db._FileManager.Find(Id) ?? throw new HttpException(string.Empty, 404);
					return File(file.Source, file.Type);
				}
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			
		}
	}
}
