using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Shop.Data
{
    /// <summary>
    /// Sản phẩm
    /// </summary>
    [Table("Product")]
    public class Product
    {
        [Key] public Guid Id { get; set; } = Guid.Empty;
        [Required] public string Name { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
		public long UnitPrice { get; set; } = 0;
        public Guid MainFile { get; set; } = Guid.Empty;
		[Required] public int TotalItem { get; set; } = 0;
        [Required] public int Discount { get; set; } = 0;
        public bool Active { get; set; } = false; // 1: Active, 0: Unactive
        public string? Category { get; set; } = string.Empty;
		public List<Comment> CommentList { get; set; }
		public List<OrderDetail> OrderDetailList { get; set; }
		public List<ProductCategory> ProductCategoryList { get; set; }
    }
	[Table("Comment")]
	public class Comment
	{
		[Required] public int TT { get; set; } = 0;
		[Required] public Guid ProductId { get; set; } = Guid.Empty;
        [Required] public DateTime CreatedDate { get; set; }
        [Required] public Guid UserId { get; set; } = Guid.Empty;
        public double Rating { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
	}
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Required] public Guid Id { get; set; } = Guid.Empty;
        [Required] public string? Name { get; set; } = string.Empty;
        [Required] public Guid Icon { get; set; } = Guid.Empty;
        [Required] public bool Active { get; set; } = false;
    }
}