using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetAllFileManager : Output
	{
		public object Data { get; set; }
		public int TotalItemCount { get; set; } = 0;
		internal override void Query_DataInput(object? input)
		{
			using (var db = new DatabaseConnection())
			{
				var files = db._FileManager.Select(e => new {e.Id, e.Name, e.CreatedDate}).ToList();
				Data = files;
				TotalItemCount = files.Count;
			}
		}
	}
}
