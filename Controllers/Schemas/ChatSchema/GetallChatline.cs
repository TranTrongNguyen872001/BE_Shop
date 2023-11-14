using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class GetallChatline
    {
        public Guid Id { get; set; } = Guid.Empty;
        public int Index { get; set; } = 0;
        public int Page { get; set; } = 0;
    }
    public class OutputGetallChatline : Output
    {
        public List<ChatLine> Chatline { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetallChatline input = (GetallChatline)ip!;
            using (var db = new DatabaseConnection())
            {
                Chatline = db._ChatLine
                        .Where(e => e.UserId == input.Id)
                        .OrderByDescending(e => e.CreatedDate)
                        .Skip((input.Page - 1) * input.Index)
                        .Take(input.Index)
                        .ToList();
                TotalItemCount = db._ProductCategory.Count();
                TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
            }
        }
    }
}