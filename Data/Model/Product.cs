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
        public string Decription { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
		public long UnitPrice { get; set; } = 0;
		[Required] public int TotalItem { get; set; } = 0;
		public List<ProductDetail> ProductDetail = new List<ProductDetail>();
    }
	/// <summary>
	/// Thông tin sản phẩm
	/// </summary>
	[Table("ProductDetail")]
	public class ProductDetail
	{
		[Key] public Guid Id { get; set; } = Guid.Empty;
		[Required] public Guid ProductId { get; set; } = Guid.Empty;
		[Required] public int TypeNumber { get; set; } = 0;
		public string TypeName { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		[Required] public int TotalItem { get; set; } = 0;
	}
}