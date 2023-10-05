using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

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
        string Picture { get; set; } = string.Empty;
		public string Decription { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
		public long UnitPrice { get; set; } = 0;
		[Required] public int TotalItem { get; set; } = 0;
    }
}