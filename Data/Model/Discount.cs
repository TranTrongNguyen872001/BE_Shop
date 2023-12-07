using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_Shop.Data
{
    /// <summary>
    /// Người dùng
    /// </summary>
    [Table("Discount")]
    public class Discount
    {
        [Key] public Guid Id { get; set; } = Guid.Empty;
        [Required] public String Code { get; set; } = string.Empty;
        public DateTime? StopDate { get; set; } = null;
        [Required] public int Value {get; set;} = 0;
    }
}