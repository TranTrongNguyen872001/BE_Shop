using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Data
{
    /// <summary>
    /// Đơn hàng
    /// </summary>
    [Table("Order")]
    public class Order
    {
        [Key] public Guid Id { get; set; } = Guid.Empty;
        [Required] public Guid UserId { get; set; } = Guid.Empty;
        [Required] public string Address { get; set; } = string.Empty;
        [Required] public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        [Required] public int Status { get; set; } = 0;
		[Required] public long Tax { get; set; } = 0;
		[Required] public long TotalPrice { get; set; } = 0;
		public List<OrderDetail> OrderDetail = new List<OrderDetail>();

	}
    /// <summary>
    /// Chi tiết đơn hàng
    /// </summary>
    [Table("OrderDetail")]
    public class OrderDetail
    {
		[Required] public Guid OrderId { get; set; } = Guid.Empty;
		[Required] public Guid ProductDetailId { get; set; } = Guid.Empty;
		[Required] public int ItemCount { get; set; } = 0;
		[Required] public long UnitPrice { get; set; } = 0;
		[Required] public long TotalPrice { get; set; } = 0;
	}
}
