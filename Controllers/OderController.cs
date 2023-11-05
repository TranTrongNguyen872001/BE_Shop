using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE_Shop.Controllers
{
	[Route("/api/order")]
	public class OrderController : BaseController
	{
		/// <summary>
		/// Thêm hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddOrder input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
			return await QueryCheck<OutputAddOrder>(input);
		}
        /// <summary>
        /// Sửa hóa đơn
        /// </summary>
        /// <param name="input"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpPut("{Id}")]
		public async Task<IActionResult> Update([FromBody] UpdateOrder input, Guid Id)
		{
            input.Id = Id;
            input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
            return await QueryCheck<OutputUpdateOrder>(input);
		}
        /// <summary>
        /// Xác nhận hóa đơn
        /// </summary>
        /// <param name="input"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
        [HttpPut("cof/{Id}")]
        public async Task<IActionResult> ConfirmOrder([FromBody] ConfirmOrder input, Guid Id)
        {
            input.Id = Id;
            input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
            return await QueryCheck<OutputConfirmOrder>(input);
        }
        /// <summary>
        /// Thực hiện quy trình thanh toán hóa đơn
        /// </summary>
        /// <param name="StatusNumber"></param>
        /// <param name="OrderId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        //     [Authorize(Roles = "Admin,Member")]
        //     [HttpPut("wf/{StatusNumber}/{OrderId}")]
        //     public async Task<IActionResult> Workflow(int StatusNumber, Guid OrderId)
        //     {
        //         return await QueryCheck<OutputWorkflowOrder>(new WorkflowOrder()
        //{
        //	Status = StatusNumber,
        //             OrderId = OrderId,
        //             UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401)),
        //	Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? throw new HttpException(string.Empty, 401)
        //         });
        //     }
        /// <summary>
        /// Xóa hóa đơn
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(Guid Id)
		{
			return await QueryCheck<OutputDeleteOrder>(Id);
		}
		/// <summary>
		/// Lấy danh sách hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("list")]
		public async Task<IActionResult> GetAll([FromBody] GetAllOrder input)
		{
			return await QueryCheck<OutputGetAllOrder>(input);
		}
        /// <summary>
        /// Lấy danh sách hóa đơn theo UserId
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("list/{Id}")]
        public async Task<IActionResult> GetAllById(Guid Id, [FromBody] GetAllOrder input)
        {
			input.UserId = Id;
            return await QueryCheck<OutputGetAllByIdOrder>(input);
        }
        /// <summary>
        /// Lấy danh sách hóa đơn của tôi
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpPost("listmine")]
		public async Task<IActionResult> GetAllMine([FromBody] GetAllOrder input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
			return await QueryCheck<OutputGetAllByIdOrder>(input);
		}
        /// <summary>
        /// Lấy thông tin hóa đơn
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneOrder>(Id);
		}
	}
}
