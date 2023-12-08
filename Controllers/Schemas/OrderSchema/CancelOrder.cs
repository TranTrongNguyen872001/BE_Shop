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
                var orderDetails = db._OrderDetail.Where(e => e.OrderId == input.OrderId).ToList();
                foreach (var item in orderDetails){
                    var product = db._Product.Find(item.ProductId);
                    if (product != null){
                        product.TotalItem += item.ItemCount;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
