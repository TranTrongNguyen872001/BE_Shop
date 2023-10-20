using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class WorkflowOrder
    {
        public int Status { get; set; } = 0;
        public Guid OrderId { get; set; } = Guid.Empty;
        internal Guid UserId { get; set; } = Guid.Empty;
    }
    public class OutputWorkflowOrder : Output
    {
        internal override void Query_DataInput(object? ip)
        {
            WorkflowOrder input = (WorkflowOrder)ip;
            using (var db = new DatabaseConnection())
            {
                var order = db._Order.Find(input.OrderId) ?? throw new HttpException(string.Empty, 404);
                if((db._User.Find(input.UserId)?.Role ?? throw new HttpException(string.Empty, 404)) == "Admin" || order.UserId == input.UserId)
                {
                    switch (order.Status)
                    {
                        case (0):
                            switch (input.Status)
                            {
                                case (1):
                                    order.Status = 1;
                                    break;
                                case (2):
                                    order.Status = 2;
                                    break;
                                default:
                                    throw new HttpException(string.Empty, 400);
                            };
                            break;
                        case (1):
                            switch (input.Status)
                            {
                                case (2):
                                    order.Status = 2;
                                    break;
                                case (3):
                                    order.Status = 3;
                                    break;
                                case (4):
                                    order.Status = 4;
                                    break;
                                default:
                                    throw new HttpException(string.Empty, 400);
                            };
                            break;
                        case (2):
                            switch (input.Status)
                            {
                                case (3):
                                    order.Status = 3;
                                    break;
                                default:
                                    throw new HttpException(string.Empty, 400);
                            };
                            break;
                        default:
                            throw new HttpException(string.Empty, 403);
                    };
                    db.SaveChanges();
                }
            }
        }
    }
}
