﻿using BE_Shop.Data;
using Newtonsoft.Json;

namespace BE_Shop.Controllers
{
	public class UpdateAddress
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Decription { get; set; } = string.Empty;
		internal Guid UserId { get; set; } = Guid.Empty;
		public string Name {get; set;} = string.Empty;
		public string Contact {get; set;} = string.Empty;
        public string? Tinh { get; set; }
        public string? Huyen { get; set; }
        public string? Xa { get; set; }
	}
	public class OutputUpdateAddress : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			UpdateAddress input = (UpdateAddress)ip!;
			using (var db = new DatabaseConnection())
			{
				var Address = db._Address.Find(input.Id) ?? throw new HttpException(string.Empty, 404);
				if (Address.UserId != input.UserId)
				{
					throw new HttpException(string.Empty, 403);
				}
				if (input.Decription != string.Empty)
				{
					Address.Description = input.Decription;
					Address.Name = input.Name;
					Address.Contact = input.Contact;
					Address.Tinh = input.Tinh;
					Address.Huyen = input.Huyen;
					Address.Xa = input.Xa;
					db.SaveChanges();
				}
			}
		}
	}
}