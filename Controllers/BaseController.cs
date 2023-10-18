using BE_Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BE_Shop.Controllers
{
	public class BaseController : ControllerBase
	{
		internal async Task<IActionResult> QueryCheck<T>(object? input) where T : Output
		{
			try
			{
				T a = Activator.CreateInstance(typeof(T)) as T;
				string ResetToken = string.Empty;
				using (var db = new DatabaseConnection())
				{
					if (User.Claims.Any())
					{
						var user = db._User.Find(Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value));
						if (user.TokenKey == User.Claims.FirstOrDefault(c => c.Type == "Key")?.Value)
						{
							a.Query_DataInput(input);
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
							ResetToken = "Bearer " + tokenHandler.WriteToken(token);
						}
						else
						{
							throw new HttpException(string.Empty, 403);
						}
					}
				}
				return Ok(new { ResetToken = ResetToken, Data = a });
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		internal async Task<IActionResult> QueryCheck_NonToken<T>(object? input) where T : Output
		{
			try
			{
				T a = Activator.CreateInstance(typeof(T)) as T;
				a.Query_DataInput(input);
				return Ok(a);
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
