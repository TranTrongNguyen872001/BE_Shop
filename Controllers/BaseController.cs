using BE_Shop.Data;
using Microsoft.AspNetCore.Mvc;

namespace BE_Shop.Controllers
{
	public class BaseController : ControllerBase
	{
		internal async Task<IActionResult> QueryCheck<T>(object? input) where T : Output
		{
			try
			{
				T? a = Activator.CreateInstance(typeof(T)) as T;
				a.Query_DataInput(input);
				return Ok(a);
			}
			catch (HttpException ex)
			{
				return StatusCode(ex.StatusCode, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(400, ex.Message);
			}
		}
	}
}
