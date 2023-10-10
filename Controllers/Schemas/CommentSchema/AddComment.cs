using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class AddComment
	{
		public Guid ProductId { get; set; } = Guid.Empty;
		public float Rating { get; set; } = 0;
		public string Description { get; set; } = string.Empty;
		internal Guid UserId { get; set; }
	}
	public class OutputAddComment : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			AddComment input = (AddComment)ip;
			using(var db = new DatabaseConnection())
			{
				db._Comment.Add(new Comment()
				{
					TT = (db._Comment.Where(e => e.ProductId == input.ProductId).Max(x => (int?)x.TT) ?? 0) + 1,
					ProductId = input.ProductId,
					Rating = input.Rating,
					Description = input.Description,
					CreatedDate = DateTime.Now,
					UserId = input.UserId
				});
				db.SaveChanges();
			}
		}
	}
}
