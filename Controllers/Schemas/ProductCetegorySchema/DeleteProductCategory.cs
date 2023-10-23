using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class OutputDeleteProductCategory : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            Guid Id = (Guid)ip!;
            using (var db = new DatabaseConnection())
            {
                db.Remove(db._ProductCategory.Find(Id) ?? throw new HttpException(string.Empty, 404));
                db.SaveChanges();
            }
        }
    }
}
