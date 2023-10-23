using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class GetallRoom
    {
        public int Index { get; set; } = 0;
        public int Page { get; set; } = 0;
    }
    public class OutputGetallRoom : Output
    {
        public object Rooms { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetallRoom input = (GetallRoom)ip!;
            using (var db = new DatabaseConnection())
            {
                Rooms = db._ChatLine
                        .GroupBy(e => new { e.UserId })
                        .Select(e => new
                        {
                            Message = db._ChatLine.Where(a => a.CreatedDate == e.Max(y => y.CreatedDate)).FirstOrDefault(),
                        })
                        .Select(e => new
                        {
                            Id = e.Message.UserId,
                            db._User.Find(e.Message.UserId).Name,
                            LastDate = e.Message.CreatedDate,
                            LastMessage = e.Message.Description,
                            LastSender = e.Message.SendedUser,
                            LastSenderRole = db._User.Find(e.Message.SendedUser).Role,
                        })
                        .OrderByDescending(e => e.LastDate)
                        .Skip((input.Page - 1) * input.Index)
                        .Take(input.Index)
                        .ToList();
                TotalItemCount = db._ProductCategory.Count();
                TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
            }
        }
    }
}
