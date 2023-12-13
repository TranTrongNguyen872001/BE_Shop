using BE_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using BE_Shop.Hubs;
using System.Net;

namespace BE_Shop.Controllers
{
	[Route("/api/payment")]
	public class PaymentController : BaseController
	{
        private readonly IHubContext<NotificationHub> _hubContext;
        public PaymentController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> Payment(Guid id, Guid Discount)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new HttpException(string.Empty, 401));
                using (var db = new DatabaseConnection())
				{
					var order = db._Order.Find(id) ?? throw new HttpException(string.Empty, 404);
                    if(order.UserId != userId || (order.Status != 0 && order.Status != 1))
                    {
                        throw new HttpException(string.Empty, 403);
                    }
                    return Ok(VnPayLibrary.GetPaymentUrl(order, Discount));
				}
            }
            catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
        }
        [AllowAnonymous]
        [HttpGet("vnpay_return")]
        public async Task<IActionResult> VnPayReturn([FromQuery] VnPayResponseModel vnPayResponse)
        {
            try{
                string queryString = "vnp_Amount=" + WebUtility.UrlEncode(vnPayResponse.vnp_Amount)
                    + "&vnp_BankCode=" + WebUtility.UrlEncode(vnPayResponse.vnp_BankCode)
                    + "&vnp_BankTranNo=" + WebUtility.UrlEncode(vnPayResponse.vnp_BankTranNo)
                    + "&vnp_CardType="+ WebUtility.UrlEncode(vnPayResponse.vnp_CardType)
                    + "&vnp_OrderInfo=" + WebUtility.UrlEncode(vnPayResponse.vnp_OrderInfo)
                    + "&vnp_PayDate=" + WebUtility.UrlEncode(vnPayResponse.vnp_PayDate)
                    + "&vnp_ResponseCode=" + WebUtility.UrlEncode(vnPayResponse.vnp_ResponseCode)
                    + "&vnp_TmnCode=" + WebUtility.UrlEncode(vnPayResponse.vnp_TmnCode)
                    + "&vnp_TransactionNo=" + WebUtility.UrlEncode(vnPayResponse.vnp_TransactionNo)
                    + "&vnp_TransactionStatus=" + WebUtility.UrlEncode(vnPayResponse.vnp_TransactionStatus)
                    + "&vnp_TxnRef=" + WebUtility.UrlEncode(vnPayResponse.vnp_TxnRef);
                
                using (var db = new DatabaseConnection())
				{
                    if (vnPayResponse.vnp_ResponseCode != "00" || VnPayLibrary.HmacSHA512(VnPayLibrary.vnp_HashSecret, queryString) != vnPayResponse.vnp_SecureHash)
                    {
                        var order = db._Order.Find(Guid.Parse(vnPayResponse.vnp_TxnRef ?? throw new HttpException(string.Empty, 400))) ?? throw new HttpException(string.Empty, 404);
                        order.Status = 2;
                        order.MethodPayment = true;
                        db.SaveChanges();
                        await _hubContext.Clients.Group(order.Id.ToString()).SendAsync("Fail");
                        return StatusCode(400, "Fail");
                    }
                    else
                    {
                        var order = db._Order.Find(Guid.Parse(vnPayResponse.vnp_TxnRef ?? throw new HttpException(string.Empty, 400))) ?? throw new HttpException(string.Empty, 404);
                        order.Status = 2;
                        order.MethodPayment = true;
                        db.SaveChanges();
                        await _hubContext.Clients.Group(order.Id.ToString()).SendAsync("PaySuccess");
                        return Ok("Success");
                    }
					
				}
            }
            catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
        }
    }
}