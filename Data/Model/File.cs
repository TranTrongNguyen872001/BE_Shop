using System.ComponentModel.DataAnnotations;

namespace BE_Shop.Data.Model
{
	public class FileManager
	{
		[Key] Guid Id { get; set; }
		[Required] string Name { get; set; }
		[Required] string Description { get; set;}
		[Required] Stream Source { get; set;}
	}
}
