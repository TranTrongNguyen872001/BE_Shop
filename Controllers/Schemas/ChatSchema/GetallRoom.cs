using System.Net;
using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace BE_Shop.Controllers
{
    public class GetallRoom
    {
        public int Index { get; set; } = 0;
        public int Page { get; set; } = 0;
    }
    public class OutputGetallRoomData1
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public OutputGetallRoomData2? Message { get; set; }
    }
    public class OutputGetallRoomData2
    {
        public Guid Id { get; set; }
        public Guid SendedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
    }
    public class OutputGetallRoom : Output
    {
        public List<OutputGetallRoomData1> Rooms { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalItemPage { get; set; }
        internal override void Query_DataInput(object? ip)
        {
            GetallRoom input = (GetallRoom)ip!;
            using (var db = new DatabaseConnection())
            {
                // Rooms = db._ChatLine
                //         .GroupBy(e => new { e.UserId })
                //         .Select(e => new
                //         {
                //             Message = db._ChatLine.Where(a => a.CreatedDate == e.Max(y => y.CreatedDate)).FirstOrDefault(),
                //         })
                //         .Select(e => new OutputGetallRoomData1
                //         {
                //             Id = e.Message.UserId,
                //             Name = db._User.Where(y => y.Id == e.Message.UserId).FirstOrDefault().Name,
                //             LastDate = e.Message.CreatedDate,
                //             LastMessage = e.Message.Description,
                //             LastSender = e.Message.SendedUser,
                //             LastSenderRole = db._User.Where(y => y.Id == e.Message.SendedUser).FirstOrDefault().Role,
                //         })
                //         .OrderByDescending(e => e.LastDate)
                //         .Skip((input.Page - 1) * input.Index)
                //         .Take(input.Index)
                //         .ToList();
                Rooms = db._User
                    .Where(e => e.Status == true)
                    .Select(e => new OutputGetallRoomData1{
                        Id = e.Id,
                        Name = e.Name,
                        Message = db._ChatLine.Where(y => y.UserId == e.Id)
                            .OrderByDescending(y => y.CreatedDate)
                            .Select(y => new OutputGetallRoomData2{
                                Id = y.Id,
                                CreatedDate = y.CreatedDate,
                                Description = y.Description,
                                SendedUser = y.SendedUser,
                            })
                            .FirstOrDefault(),
                    })
                    .Skip((input.Page - 1) * input.Index)
                    .Take(input.Index)
                    .ToList();
                TotalItemCount = db._ProductCategory.Count();
                TotalItemPage = (int)Math.Ceiling((float)TotalItemCount / (float)input.Index);
            }
        }
    }
}
