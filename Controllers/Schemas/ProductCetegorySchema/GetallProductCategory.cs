using BE_Shop.Data;
using System.Linq;

namespace BE_Shop.Controllers
{
    public class GetallProductCategory
    {
        public int Index { get; set; } = 0;
        public int Page { get; set; } = 0;
    }
    public class OutputGetallAdminProductCategoryData1
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid Icon { get; set; }
        public bool Active { get; set; }
        public int TotalProduct { get; set; }
        public long TotalProfit { get; set; } = 0;
    }
    public class OutputGetallProductCategory : Output
    {
        public List<OutputGetallAdminProductCategoryData1> ProductCategory { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetallProductCategory input = (GetallProductCategory)ip!;
            using (var db = new DatabaseConnection())
            {
                ProductCategory = db._ProductCategory
                        .Where(e => e.Active == true)
                        .Select(e => new OutputGetallAdminProductCategoryData1
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Icon = e.Icon,
                            Active = e.Active,
                            TotalProduct = db._Product.Where(y => y.Category != null && y.Category.Contains(e.Id.ToString())).Count()
                        })
                        .Skip((input.Page - 1) * input.Index)
                        .Take(input.Index)
                        .ToList();
                TotalItemCount = db._ProductCategory.Count();
                TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
            }
        }
    }
    public class OutputGetallAdminProductCategory : Output
    {
        public List<OutputGetallAdminProductCategoryData1> ProductCategory { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetallProductCategory input = (GetallProductCategory)ip!;
            using (var db = new DatabaseConnection())
            {
                ProductCategory = db._ProductCategory
                        .Select(e => new OutputGetallAdminProductCategoryData1
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Icon = e.Icon,
                            Active = e.Active,
                            TotalProduct = db._Product.Where(y => y.Category != null && y.Category.Contains(e.Id.ToString())).Count(),
                            TotalProfit = db._OrderDetail
                                .Join(db._Product,
                                    a => a.ProductId,
                                    b => b.Id,
                                    (a, b) => new
                                    {
                                        a.UnitPrice,
                                        a.ItemCount,
                                        b.Category
                                    })
                                .Where(y => y.Category != null && y.Category.Contains(e.Id.ToString()))
                                .Select(y => y.UnitPrice * y.ItemCount)
                                .Sum()
                        })
                        .Skip((input.Page - 1) * input.Index)
                        .Take(input.Index)
                        .ToList();
                TotalItemCount = db._ProductCategory.Count();
                TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
            }
        }
    }
}
