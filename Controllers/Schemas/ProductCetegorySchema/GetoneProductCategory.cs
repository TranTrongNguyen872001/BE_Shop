using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class OutputGetoneProductCategory : Output
    {
        public ProductCategory ProductCategory { get; set; }
        internal override void Query_DataInput(object? input)
        {
            Guid id = (Guid)input!;
            using (var db = new DatabaseConnection())
            {
                ProductCategory = db._ProductCategory.Find(id) ?? throw new HttpException(string.Empty, 404);
            }
        }
    }
}
