﻿using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class ConfirmOrder
    {
        internal Guid Id { get; set; } = Guid.Empty;
        internal Guid UserId { get; set; } = Guid.Empty;
        public string Address { get; set; } = string.Empty;
        public bool MethodPayment { get; set; } = false;
        public string ReceiveName { get; set; } = string.Empty;
        public string ReceiveContact { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Tinh { get; set; }
        public string? Phuong { get; set; }
        public string? Xa { get; set; }
        public Guid? DiscountId { get; set; }
    }
    public class OutputConfirmOrder : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            ConfirmOrder input = (ConfirmOrder)ip!;
            using (var db = new DatabaseConnection())
            {
                var Order = db._Order.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
                if (Order.Status != 0 || Order.UserId != input.UserId)
                {
                    throw new HttpException(string.Empty, 403);
                }
                Order.Address = input.Address;
                Order.MethodPayment = input.MethodPayment;
                Order.ReceiveName = input.ReceiveName;
                Order.ReceiveContact = input.ReceiveContact;
                Order.CreatedDate = DateTime.Now;
                Order.DiscountId = input.DiscountId;
                Order.Email = input.Email;
                Order.Tinh = input.Tinh;
                Order.Phuong = input.Phuong;
                Order.Xa = input.Xa;
                if(input.MethodPayment == true)
                {
                    Order.Status = 2;
                }
                else
                {
                    Order.Status = 1;
                }
                db.SaveChanges();
                var od = db._OrderDetail.Where(e => e.OrderId == input.Id).ToList();
                if(od.Any())
                {
                    foreach(var orderDetail in od)
                    {
                        var product = db._Product.Find(orderDetail.ProductId);
                        orderDetail.UnitPrice = product == null ? 0 : product.UnitPrice - product.Discount;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
