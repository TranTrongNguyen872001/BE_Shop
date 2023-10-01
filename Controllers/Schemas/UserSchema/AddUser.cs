using BE_Shop.Data;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
	}
    public class OutputAddUser : Output
    {
        public string Token { get; set; } = string.Empty;
        internal override void Query_DataInput(object? ip)
        {
			AddUser input = (AddUser)ip;
            Guid Userid;
			using (var db = new DatabaseConnection())
            {
                if(db._User.Where(b => b.UserName == input.UserName).ToList().Count != 0)
                {
                    throw new HttpException("Dữ liệu đã tồn tại", 409);
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
				Userid = Guid.NewGuid();
                db._User.Add(new User()
				{
					Id = Userid,
					Name = input.Name,
					AddressList = addresses,
					UserName = input.UserName,
					Password = Converter.MD5Convert(input.Password),
                    Role = "Member",
				});
                db.SaveChanges();
			}
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, Userid.ToString()),
					new Claim(ClaimTypes.Role, "Member"),
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