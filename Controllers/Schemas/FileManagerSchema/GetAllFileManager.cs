using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class OutputGetAllFileManagerData1
	{
		public Guid Id {get; set;}
		public string Name {get; set;}
		public DateTime CreatedDate {get; set;}
	}
	public class OutputGetAllFileManager : Output
	{
		public List<OutputGetAllFileManagerData1> Data { get; set; }
		public int TotalItemCount { get; set; } = 0;
		internal override void Query_DataInput(object? input)
		{
			using (var db = new DatabaseConnection())
			{
				Data = db._FileManager.Select(e => new OutputGetAllFileManagerData1{Id = e.Id, Name = e.Name, CreatedDate = e.CreatedDate}).ToList();
				TotalItemCount = Data.Count;
			}
		}
	}
}
