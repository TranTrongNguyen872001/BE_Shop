using BE_Shop.Data;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_Shop.Controllers
{

    public class AddUser
    {
        /// <summary>
        /// Tên người dùng
        /// </summary>
        [Required] public string Name { get; set; } = string.Empty;
        /// <summary>
        /// danh sách địa chỉ
        /// </summary>
        public List<string> AddressList { get; set; } = new List<string>();
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required] public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required] public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Lặp lại mật khẩu
        /// </summary>
        [Required] public string ConfinPassword { get; set; } = string.Empty;
    }
    public class OutputAddUser : Output
    {
        public string Token { get; set; } = string.Empty;
        internal override void Query_DataInput(object? ip)
        {

        }
    }
}