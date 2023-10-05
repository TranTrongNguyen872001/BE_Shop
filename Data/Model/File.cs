using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BE_Shop.Data.Model
{
	public class FileManager
	{
		[Key] public Guid Id { get; set; }
		[Required] public string Name { get; set; }
		[Required] public string Description { get; set;}
		[Required] public Byte[] Source { get; set;}
	}
}
