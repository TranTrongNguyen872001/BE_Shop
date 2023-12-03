using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace BE_Shop.Controllers
{
	public class GetAllProduct
	{
		/// <summary>
		/// Số item trên 1 trang
		/// </summary>
		public int Index { get; set; } = 0;
		/// <summary>
		/// Số trang
		/// </summary>
		public int Page { get; set; } = 0;
		/// <summary>
		/// Sắp xếp theo
		/// </summary>
		public string? SortBy { get; set; }
		/// <summary>
		/// Giảm dẩn? Mặc định = false
		/// </summary>
		public bool Desc { get; set; } = false;
		/// <summary>
		/// Nhập giá trị tìm kiếm
		/// </summary>
		public string Search { get; set; } = string.Empty;
		public Guid? Category { get; set; } = null;
		public bool? Active {get; set;} = null;
	}
	public class OutputGetAllProductData1
	{
		public string Code { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid MainFile { get; set; }
		public double Rating { get; set; }
		public long UnitPrice { get; set; }
		public int TotalItem { get; set; }
		public bool Active { get; set; }
		public int Discount {get; set;}
		public List<ProductCategory> Category{ get; set; }
	}
	public class OutputGetAllProduct : Output
	{
		public List<OutputGetAllProductData1> ProductList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			GetAllProduct input = (GetAllProduct)ip!;
			using (DatabaseConnection db = new DatabaseConnection())
			{
				var temp = input.Desc ? db._Product.OrderBy(e => EF.Property<object>(e, input.SortBy ?? "Name")) : db._Product.OrderByDescending(e => EF.Property<object>(e, input.SortBy ?? "Name"));

                ProductList = temp
					.Where(e => (e.Active == true) 
						&& (input.Search == string.Empty || e.Name.Contains(input.Search))
						&& (input.Category == null || e.Category.Contains(input.Category.ToString()))
						&& (input.Active == null || e.Active == input.Active))
					.Select(e => new OutputGetAllProductData1
					{
						Code = e.Code,
						Id = e.Id,
						Name = e.Name,
						Active = e.Active,
						MainFile = e.MainFile != Guid.Empty ? e.MainFile : db._FileManager.Where(y => y.OwnerId == e.Id).Select(y => y.Id).FirstOrDefault(),
						Rating = Math.Round((db._Comment.Where(y => y.ProductId == e.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2,
						UnitPrice = e.UnitPrice,
						TotalItem = e.TotalItem,
						Discount = e.Discount,
                        Category = db._ProductCategory
                            .Where(y => e.Category != null && e.Category.Contains(y.Id.ToString()))
                            .ToList(),
                    })
					.Skip((input.Page - 1) * input.Index)
					.Take(input.Index)
					.ToList();
#pragma warning restore CS8604 // Possible null reference argument.
                TotalItemCount = db._Product
						.Where(e => input.Search == string.Empty
							|| input.Search.Contains(e.Name)).Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
    public class OutputGetAllAdminProduct : Output
    {
        public List<OutputGetAllProductData1> ProductList { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetAllProduct input = (GetAllProduct)ip!;
            using (var db = new DatabaseConnection())
            {
                var temp = input.Desc ? db._Product.OrderBy(e => EF.Property<object>(e, input.SortBy ?? "Name")) : db._Product.OrderByDescending(e => EF.Property<object>(e, input.SortBy ?? "Name"));
                ProductList = temp
					#pragma warning disable CS8604 // Possible null reference argument.
                    .Where(e => (input.Search == string.Empty || e.Name.Contains(input.Search))
						&& (input.Category == null || e.Category.Contains(input.Category.ToString()))
						&& (input.Active == null || e.Active == input.Active))
                    .Select(e => new OutputGetAllProductData1
					{
						Code = e.Code,
						Id = e.Id,
						Name = e.Name,
						Active = e.Active,
						MainFile = e.MainFile != Guid.Empty ? e.MainFile : db._FileManager.Where(y => y.OwnerId == e.Id).Select(y => y.Id).FirstOrDefault(),
						Rating = Math.Round((db._Comment.Where(y => y.ProductId == e.Id).Average(y => (double?)y.Rating) ?? 0) * 2, 0, MidpointRounding.ToPositiveInfinity) / 2,
						UnitPrice = e.UnitPrice,
						TotalItem = e.TotalItem,
						Discount = e.Discount,
                        Category = db._ProductCategory
                            .Where(y => e.Category != null && e.Category.Contains(y.Id.ToString()))
                            .ToList(),
                    })
                    .Skip((input.Page - 1) * input.Index)
                    .Take(input.Index)
                    .ToList();
                TotalItemCount = db._Product
                        .Where(e => input.Search == string.Empty
                            || input.Search.Contains(e.Name)).Count();
                TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
            }
        }
    }
}