using BE_Shop.Data;
using BE_Shop.Data.Service;

namespace BE_Shop.Controllers
{
    public class OutputSendValidFinishOrder : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            (Guid OrderId, ISMSService smsService) input = ((Guid, ISMSService))ip!;
            int ValidCode = new Random().Next(0, 1000000);
			using (var db = new DatabaseConnection())
			{
                var Order = db._Order.Find(input.OrderId) ?? throw new HttpException(string.Empty, 404);
				var user = db._User.Find(Order.UserId) ?? throw new HttpException(string.Empty, 404);
				user.ValidCode = ValidCode;
				db.SaveChanges();
				input.smsService.SendSMS(Order.ReceiveContact, ValidCode.ToString("000000") + " la ma xac thuc cua quy khach");
			}
        }
    }
    public class OutputFinishOrder : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            Guid input = (Guid)ip!;
            using (var db = new DatabaseConnection())
            {
                var Order = db._Order.Find(input) ?? throw new HttpException(string.Empty, 404);
                if (Order.Status != 1 && Order.Status != 2)
                {
                    throw new HttpException(string.Empty, 403);
                }
                Order.Status = 3;
                db.SaveChanges();
            }
        }
    }
}
