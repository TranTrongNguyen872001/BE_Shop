using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class OutputCancelOrder : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            (Guid OrderId, Guid UserId) input = ((Guid, Guid))ip!;
            using (var db = new DatabaseConnection())
            {
                var Order = db._Order.Find(input.OrderId) ?? throw new HttpException(string.Empty, 404);
                if (input.UserId != Order.UserId || (Order.Status != 1 && Order.Status != 2))
                {
                    throw new HttpException(string.Empty, 403);
                }
                Order.Status = 4;
                db.SaveChanges();
            }
        }
    }
}
