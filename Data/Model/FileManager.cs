using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BE_Shop.Data
{
	public class FileManager
	{
		[Key] public Guid Id { get; set; } = Guid.Empty;
		[Required] public Guid OwnerId { get; set; } = Guid.Empty;
		[Required] public string Name { get; set; } = string.Empty;
		[Required] public string Type { get; set; } = string.Empty;
		[Required] public DateTime CreatedDate { get; set;}
		[Required] public byte[] Source { get; set;}
	}
}
