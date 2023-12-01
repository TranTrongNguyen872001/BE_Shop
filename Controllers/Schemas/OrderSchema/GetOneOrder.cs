using BE_Shop.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace BE_Shop.Controllers
{
	public class OutputGetOneOrderData1
	{
		public Guid Id { get; set; }
		public string Address { get; set; }
		public string ReceiveName { get; set; }
		public string ReceiveContact { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string MethodPayment { get; set; }
		public int Status { get; set; }
		public OutputGetOneOrderData2? User { get; set; }
		public long TotalPrice { get; set; }
		public List<OutputGetOneOrderData3> Detail { get; set; }
	}
	public class OutputGetOneOrderData2
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string Role { get; set; }
		public string Email { get; set; }
	}
	public class OutputGetOneOrderData3
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public long UnitPrice { get; set; }
		public long ItemCount { get; set; }
		public long TotalPrice { get; set; }
	}
	public class OutputGetOneOrder : Output
	{
		public OutputGetOneOrderData1 Order { get; set; }
		internal override void Query_DataInput(object? ip)
		{
			Guid Id = (Guid)ip!;
			using (var db = new DatabaseConnection())
			{
                Order = db._Order
					.OrderByDescending(e => e.CreatedDate)
					.Where(e => e.Id == Id)
					.Select(e => new OutputGetOneOrderData1
					{
						Id = e.Id,
						Address = e.Address,
						CreatedDate = e.CreatedDate,
						MethodPayment = e.MethodPayment ? "Online" : "Offline",
						ReceiveName = e.ReceiveName,
						ReceiveContact = e.ReceiveContact,
						Status = e.Status,
						User = db._User.Where(y => y.Id == e.UserId).Select(y => new OutputGetOneOrderData2
						{
							Id = e.UserId,
                        	Name = y.Name,
                        	Role = y.Role,
							Email = y.UserName
						}).FirstOrDefault(),
						TotalPrice = db._OrderDetail
							.Where(y => y.OrderId == e.Id)
							.Select(y => y.UnitPrice * y.ItemCount)
							.Sum(),
						Detail = db._OrderDetail
							.Join(db._Product, x => x.ProductId, y => y.Id, (x,y) => new {
								x.OrderId,
								x.ProductId,
								x.ItemCount,
								x.UnitPrice,
								PU = y.UnitPrice,
								PD = y.Discount,
								PN = y.Name,
							})
							.Where(y => y.OrderId == Id)
							.Select(y => new OutputGetOneOrderData3
							{
								ProductId = y.ProductId,
								ProductName = y.PN,
								UnitPrice = e.Status != 0 ? y.UnitPrice : y.PU - y.PD,
								ItemCount = y.ItemCount,
								TotalPrice = (e.Status != 0 ? y.UnitPrice : y.PU - y.PD) * y.ItemCount,
							})
							.ToList(),
                    })
					.FirstOrDefault() ?? throw new HttpException(string.Empty, 404);
			}
		}
	}
}