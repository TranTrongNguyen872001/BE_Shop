using BE_Shop.Data;
using System.Linq;

namespace BE_Shop.Controllers
{
    public class GetallProductCategory
    {
        public int Index { get; set; } = 0;
        public int Page { get; set; } = 0;
    }
    public class OutputGetallProductCategory : Output
    {
        public object ProductCategory { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetallProductCategory input = (GetallProductCategory)ip!;
            using (var db = new DatabaseConnection())
            {
                ProductCategory = db._ProductCategory
                        .Select(e => new
                        {
                            e.Id,
                            e.Name,
                            e.Icon,
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
