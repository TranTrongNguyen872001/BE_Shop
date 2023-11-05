using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class UpdateProductCategory
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid Icon { get; set; } = Guid.Empty;
        public bool Active { get; set; } = false;
    }
    public class OutputUpdateProductCategory : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            UpdateProductCategory input = (UpdateProductCategory)ip!;
            using (var db = new DatabaseConnection())
            {
                var temp = db._ProductCategory.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
                temp.Name = input.Name;
                temp.Icon = input.Icon;
                temp.Active = input.Active;
                db.SaveChanges();
            }
        }
    }
}
