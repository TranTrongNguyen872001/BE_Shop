using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class AddProductCategory
    {
        public string Name { get; set; } = string.Empty;
        public Guid Icon { get; set; } = Guid.Empty;
    }
    public class OutputAddProductCategory : Output
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        internal override void Query_DataInput(object? ip)
        {
            AddProductCategory input = (AddProductCategory)ip!;
            using (var db = new DatabaseConnection())
            {
                db._ProductCategory.Add(new ProductCategory()
                {
                    Id = this.Id,
                    Name = input.Name,
                    Icon = input.Icon,
                });
                db.SaveChanges();
            }
        }
    }
}
