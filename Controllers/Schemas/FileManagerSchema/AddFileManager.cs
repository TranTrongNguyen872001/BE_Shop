using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputAddFileManager : Output
	{
		public List<object> Data { get; set; } = new List<object>();
		public int TotalUpload { get; set; } = 0;
		public int TotalSuccess { get; set; } = 0;
		internal override void Query_DataInput(object? ip)
		{
			List<IFormFile> input = (List<IFormFile>)ip;
			TotalUpload = input.Count;
			using (var db = new DatabaseConnection())
			{
				foreach (var file in input)
				{
					if(file.ContentType.StartsWith("image") && file.Length > 0)
					{
						Guid id = Guid.NewGuid();
						using (var ms = new MemoryStream())
						{
							file.CopyTo(ms);
							db._FileManager.Add(new FileManager()
							{
								Id = id,
								Name = file.FileName,
								CreatedDate = DateTime.Now,
								OwnerId = Guid.Empty,
								Source = ms.ToArray(),
								Type = file.ContentType,
							});
							db.SaveChanges();
						}
						Data.Add(new { Id = id, Name = file.FileName });
						TotalSuccess++;
					}
				}
			}
		}
	}
}
