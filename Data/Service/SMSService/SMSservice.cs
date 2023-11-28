using System.Net;
using System.Collections.Specialized;
 
namespace BE_Shop.Data.Service
{
	public class  SMSService : ISMSService
	{       
		public async void SendSMS(string PhoneNumber, string Message)
		{
            using (var wb = new WebClient())
            {
                var response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
                {
                    {"apikey" , "NzM3MTVhNDk1OTc3Nzk2YzU3NTM0NDc4NTUzNTY3Nzc="},
                    {"numbers" , PhoneNumber},
                    {"message" , Message},
                    {"sender" , "MSS"}
                });
            }
		}  
	}
}