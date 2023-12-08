namespace BE_Shop.Data.Service
{
	public class  SMSService : ISMSService
	{       
		public async void SendSMS(string PhoneNumber, string Message)
		{
            using (var wb = new HttpClient())
            {
                var response = await wb.PatchAsync("https://api.txtlocal.com/send/", null);
            }
		}  
	}
}