using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BE_Shop.Data.Service;

namespace BE_Shop.Controllers
{
	[Route("/api/order")]
	public class OrderController : BaseController
	{
		private readonly ISMSService smsService;
		public OrderController(ISMSService smsService)
		{
			this.smsService = smsService;
		}
		/// <summary>
		/// Thêm hóa đơn
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Member")]
		[HttpPost]
		[ProducesResponseType(typeof(OutputAddOrder), 200)]
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
		[ProducesResponseType(typeof(OutputUpdateOrder), 200)]
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
		[ProducesResponseType(typeof(OutputConfirmOrder), 200)]
        public async Task<IActionResult> ConfirmOrder([FromBody] ConfirmOrder input, Guid Id)
        {
            input.Id = Id;
            input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
            return await QueryCheck<OutputConfirmOrder>(input);
        }
		/// <summary>
        /// Hoàn tất hóa đơn
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
        [HttpPut("fin/{Id}")]
		[ProducesResponseType(typeof(OutputFinishOrder), 200)]
        public async Task<IActionResult> FinistOrder(Guid Id)
        {
        	return await QueryCheck<OutputFinishOrder>(Id);
        }
		/// <summary>
        /// Hủy hóa đơn
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
        [HttpPut("can/{Id}")]
		[ProducesResponseType(typeof(OutputCancelOrder), 200)]
        public async Task<IActionResult> CancelOrder(Guid Id)
        {
            return await QueryCheck<OutputCancelOrder>((Id, Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401))));
        }
        /// <summary>
        /// Xóa hóa đơn
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
		[HttpDelete("{Id}")]
		[ProducesResponseType(typeof(OutputDeleteOrder), 200)]
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
		[ProducesResponseType(typeof(OutputGetAllOrder), 200)]
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
		[ProducesResponseType(typeof(OutputGetAllOrderById), 200)]
        public async Task<IActionResult> GetAllById(Guid Id, [FromBody] GetAllOrder input)
        {
			input.UserId = Id;
            return await QueryCheck<OutputGetAllOrderById>(input);
        }
        /// <summary>
        /// Lấy danh sách hóa đơn của tôi
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpPost("listmine")]
		[ProducesResponseType(typeof(OutputGetAllOrderById), 200)]
		public async Task<IActionResult> GetAllMine([FromBody] GetAllOrder input)
		{
			input.UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
			return await QueryCheck<OutputGetAllOrderById>(input);
		}
        /// <summary>
        /// Lấy thông tin hóa đơn
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Member")]
		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(OutputGetOneOrder), 200)]
		public async Task<IActionResult> GetOne(Guid Id)
		{
			return await QueryCheck<OutputGetOneOrder>(Id);
		}
	}
}
