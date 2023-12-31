﻿using System.ComponentModel.DataAnnotations.Schema;
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
        [Required] public string Email {get; set;} = string.Empty;
        [Required] public string Address { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; } = null;
        [Required] public int Status { get; set; } = 0; //0: Khởi tạo; 1: Xác nhận; 2: Thanh toán; 3: Hoàn tất; 4: Hủy
        [Required] public bool MethodPayment { get; set; } = false; // true: online; false: offline
        [Required] public string ReceiveName { get; set; } = string.Empty;
        [Required] public string ReceiveContact { get; set; } = string.Empty;
        public Guid? DiscountId {get; set;}
        public string? Tinh { get; set; }
        public string? Huyen { get; set; }
        public string? Xa { get; set; }
        public List<OrderDetail> OrderDetail {get; set;}
	}
    /// <summary>
    /// Chi tiết đơn hàng
    /// </summary>
    [Table("OrderDetail")]
    public class OrderDetail
    {
		[Required] public Guid OrderId { get; set; } = Guid.Empty;
		[Required] public Guid ProductId { get; set; } = Guid.Empty;
		[Required] public int ItemCount { get; set; } = 0;
		[Required] public int UnitPrice { get; set; } = 0;
	}
}
