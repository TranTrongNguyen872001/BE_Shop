using BE_Shop.Data;

namespace BE_Shop.Controllers
{
	public class AddDiscount
	{
        public DateTime StopDate { get; set;} = DateTime.Now.AddDays(1);
        public int Value {get; set;} = 0;
	}
	public class OutputAddDiscount : Output
	{
		public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = Converter.RamdomByte(6);
		internal override void Query_DataInput(object? ip)
		{
			AddDiscount input = (AddDiscount)ip!;
			using (var db = new DatabaseConnection())
			{
                db._Discount.Add(new Discount()
				{
					Id = this.Id,
					Code = this.Code,
					StopDate = input.StopDate,
                    Value = input.Value,
					Status = true,
				});
				db.SaveChanges();
			}
		}
	}
}