using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class UpdateOrder
	{
		public Guid Id { get; set; }
		public int Status { get; set; }
	}
	public class OutputUpdateOrder : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateOrder input = (UpdateOrder)ip;
			using (var db = new DatabaseConnection())
			{
				var Order = db._Order.Where(e => e.Id == input.Id).FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
				Order.Status = input.Status;
				db.SaveChanges();
			}
		}
	}
}