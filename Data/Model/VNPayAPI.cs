using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BE_Shop.Data
{
	internal class VnPayLibrary
    {
        internal static string VERSION = "2.1.0";
        internal static string vnp_TmnCode = "62ROTC32";
        internal static string vnp_HashSecret = "GSDAUPNZYRUQEFRQCYKISPRCWKZUMLDR";
		internal static string vnp_URL = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private SortedList<string, string> _requestData = new SortedList<string, string>();

        internal void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

		internal static string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }

        internal string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            string signData = queryString;
            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }
            //string vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + HmacSHA512(vnp_HashSecret, signData);

            return baseUrl;
        }

		internal static string GetPaymentUrl(Order order, Guid Discount){
			VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            //vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang: " + order.Id);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", "http://backend.misaproject.click/api/payment/vnpay_return");
            vnpay.AddRequestData("vnp_TxnRef", order.Id.ToString());
			using (var db = new DatabaseConnection())
			{
            	vnpay.AddRequestData("vnp_Amount", ((
					db._OrderDetail
						.Join(db._Product, x => x.ProductId, y => y.Id, (x, y) => new {
							x.OrderId,
							x.ItemCount,
							y.UnitPrice,
							y.Discount
						})
						.Where(e => e.OrderId == order.Id)
						.Select(e => (e.UnitPrice - e.Discount) * e.ItemCount)
						.Sum()
					- db._Discount
						.Where(y => y.Id == Discount)
						.Select(y => y.Value)
						.FirstOrDefault()
				) * 100).ToString());
			}
            return vnpay.CreateRequestUrl(vnp_URL, vnp_HashSecret);
		}
    }
}
