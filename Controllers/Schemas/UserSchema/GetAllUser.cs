using BE_Shop.Data;

namespace BE_Shop.Controllers
{
    public class GetAllUser
    {

    }
    public class OutputGetAllUser : Output
    {
        public List<User> users { get; set; }
        internal override void Query_DataInput(object? ip)
        {

        }
    }
}