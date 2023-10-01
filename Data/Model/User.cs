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
        [Required][StringLength(50)] public string Name { get; set; } = string.Empty;
        [Required] public string Role { get; set; } = string.Empty;
        [Required]public string UserName { get; set; } = string.Empty;
        [Required]public string Password { get; set; } = string.Empty;
		public List<Address> AddressList { get; set; } = new List<Address>();
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
    }
}