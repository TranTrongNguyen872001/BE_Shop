using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BE_Shop.ViewModels
{
    public class UploadViewModel
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
