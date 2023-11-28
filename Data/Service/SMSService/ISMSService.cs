namespace BE_Shop.Data.Service
{
    public interface ISMSService
	{
		void SendSMS(string PhoneNumber, string Message);
	}
}