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
	public class OutputLogin : Output
    {
        public string Token { get; set; } = string.Empty;
		public OutputGetOneUser User { get; set; } = new OutputGetOneUser();
		internal override void Query_DataInput(object? ip)
        {
            Login input = (Login)ip;
            using (var db = new DatabaseConnection())
            {
                var i = Converter.MD5Convert(input.Password);

                var user = db._User.Where(b => b.UserName == input.Username && (b.Role == "NotValid" || b.Password == i)).FirstOrDefault() ?? throw new HttpException(string.Empty, 403);
                user.TokenKey = Converter.RamdomByte(32);
                db.SaveChanges();
				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, user.Id.ToString()),
						new Claim(ClaimTypes.Role, user.Role),
						new Claim("Key", user.TokenKey),
					}),
					Expires = DateTime.Now.AddMinutes(5),
					SigningCredentials = new SigningCredentials(
						new SymmetricSecurityKey(Encoding.ASCII.GetBytes(UserController.key)),
						SecurityAlgorithms.HmacSha256Signature)
				});
                Token = "Bearer " + tokenHandler.WriteToken(token);
				User.Query_DataInput(user.Id);
			}
        }
    }
}