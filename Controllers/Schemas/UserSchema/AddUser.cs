using BE_Shop.Data;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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
        //[Required] public string Password { get; set; } = string.Empty;
	}
    public class OutputAddUser : Output
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = string.Empty;
        internal override void Query_DataInput(object? ip)
        {
			AddUser input = (AddUser)ip;
            if(!Regex.Match(input.UserName, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.IgnoreCase).Success)
            {
				throw new HttpException("Email không hợp lệ", 400);
			}
			string TokenKey = Converter.RamdomByte(32);
			using (var db = new DatabaseConnection())
            {
                if(db._User.Where(b => b.UserName == input.UserName).Any())
                {
                    throw new HttpException(string.Empty, 409);
                }
                List<Address> addresses = new List<Address>();
                foreach (var address in input.AddressList)
                {
                    addresses.Add(new Address()
                    {
                        Id = Guid.NewGuid(),
                        Description = address,
                    });
                }
				db._User.Add(new User()
                {
                    Id = Id,
                    Name = input.Name,
                    AddressList = addresses,
                    UserName = input.UserName,
                    //Password = Converter.MD5Convert(input.Password),
                    Role = "NotValid",
                    TokenKey = TokenKey,
                });
                db.SaveChanges();
            }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Id.ToString()),
                    new Claim(ClaimTypes.Role, "NotValid"),
                    new Claim("Key", TokenKey),
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(UserController.key)),
                    SecurityAlgorithms.HmacSha256Signature)
            });
            Token = "Bearer " + tokenHandler.WriteToken(token);
        }
    }
}