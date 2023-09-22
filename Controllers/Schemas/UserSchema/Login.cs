using BE_Shop.Data;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_Shop.Controllers
{
    public class Login
    {
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    public class Output_Login : Output
    {
        public string Token { get; set; } = string.Empty;
        internal override void Query_DataInput(object? ip)
        {
            Login input = (Login)ip;
            using (var db = new DatabaseConnection())
            {
                User user = db._User.Where(b => b.UserName == input.Username && b.Password == input.Password).Take(1).ToList()[1] ?? throw new Exception("Đăng nhập không hợp lệ!");
                var key = Encoding.ASCII.GetBytes(UserController.key);
                string id = user.Id.ToString();
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", id)
                    }),
                    Expires = DateTime.Now.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                Token = tokenHandler.WriteToken(token);
            }
        }
    }
}