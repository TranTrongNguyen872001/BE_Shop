﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Shop.Data
{
    /// <summary>
    /// Người dùng
    /// </summary>
    [Table("User")]
    public class User
    {
        [Key] public Guid Id { get; set; } = Guid.Empty;
        [Required][StringLength(50)] public string? Name { get; set; } = string.Empty;
        [Required] public string Role { get; set; } = string.Empty;
        [Required]public string UserName { get; set; } = string.Empty;
        [Required]public string Password { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;
        public byte[]? ProPic { get; set; } = null;
        public string? ProPicType { get; set; } = null;
        public string TokenKey { get; set; } = string.Empty;
        public int? ValidCode { get; set; } = null;
        public bool? Gender { get; set; } = null;
        public DateTime? Birthday { get; set; } = null;
        [Required]public bool Status{get; set;} = true; // 1: Active, 0: Inactive
        public List<Address> AddressList { get; set; } = new List<Address>();
        
        public List<ChatLine> ChatLineList { get; set; }
        //public List<Order> OrderList { get; set; }
    }
    /// <summary>
    /// Địa chỉ
    /// </summary>
    [Table("Address")]
    public class Address
    {
        [Key] public Guid Id { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        public string Name {get; set;} = string.Empty;
        public string Contact {get; set;} = string.Empty;
        public string? Tinh { get; set; }
        public string? Huyen { get; set; }
        public string? Xa { get; set; }
    }
    [Table("ChatLine")]
    public class ChatLine
    {
        [Key] public Guid Id { get; set; } = Guid.Empty;
        [Required] public Guid UserId { get; set; } = Guid.Empty;
        [Required] public Guid SendedUser { get; set; } = Guid.Empty;
        [Required] public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required] public string Description { get; set; } = string.Empty;
    }
}