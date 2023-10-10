using BE_Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace BE_Shop.Controllers
{
	public class GetAllComment
	{
		public int Index { get; set; } = 0;
		public int Page { get; set; } = 0;
		public Guid ProductId { get; set; } = Guid.Empty;
	}
	public class OutputGetAllComment : Output
	{
		public object CommentList { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalItemPage { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			GetAllComment input = (GetAllComment)ip;
			using (var db = new DatabaseConnection())
			{
				CommentList = db._Comment
						.OrderByDescending(e => e.TT)
						.Where(e => e.ProductId == input.ProductId)
						.Skip((input.Page - 1) * input.Index)
						.Take(input.Index)
						.Select(e => new
						{
							e.Rating,
							User = new { Id = e.UserId, Name = db._User.Where(y => y.Id == e.UserId).FirstOrDefault().Name },
							e.CreatedDate,
							e.Description
						})
						.ToList();
				TotalItemCount = db._Comment
						.Where(e => e.ProductId == input.ProductId).Count();
				TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
			}
		}
	}
}
