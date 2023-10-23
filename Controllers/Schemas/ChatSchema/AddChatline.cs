using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class AddChatline
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public string Description { get; set; } = string.Empty;
        internal Guid SendedUser { get; set; } = Guid.Empty;
    }
    public class OutputAddChatline : Output
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        internal override void Query_DataInput(object? ip)
        {
            AddChatline input = (AddChatline)ip!;
            using (var db = new DatabaseConnection())
            {
                db._ChatLine.Add(new ChatLine()
                {
                    Id = this.Id,
                    Description = input.Description,
                    UserId = input.UserId,
                    CreatedDate = DateTime.Now,
                    SendedUser = input.SendedUser,
                });
            }
        }
    }
}
