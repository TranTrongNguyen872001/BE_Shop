using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BE_Shop.Data
{
    public class DatabaseConnection: DbContext
	{
		public DbSet<User> _User { get; set; }
		public DbSet<Address> _Address { get; set; }
		public DbSet<Product> _Product { get; set; }
		public DbSet<Order> _Order { get; set; }
		public DbSet<OrderDetail> _OrderDetail { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(
				@"Data Source=KAITOKIDS872001;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderDetail>()
				.HasKey(nameof(OrderDetail.ProductId), nameof(OrderDetail.OrderId));
		}
		//xóa database và tạo lại :) đỡ tốn thời gian :( mệt quá!
		public static async Task CreateDatabase()
		{
			using (var dbcontext = new DatabaseConnection())
			{
				await dbcontext.Database.EnsureDeletedAsync();
				Console.WriteLine($"CSDL {dbcontext.Database.GetDbConnection().Database} : {(await dbcontext.Database.EnsureCreatedAsync() ? "Success" : "Fail")}");
			}
		}
	}
	internal class Converter
	{
		static public string MD5Convert(string str)
		{
			//Tạo MD5 
			MD5 mh = MD5.Create();
			//Chuyển kiểu chuổi thành kiểu byte
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
			//mã hóa chuỗi đã chuyển
			byte[] hash = mh.ComputeHash(inputBytes);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
		}
	}
	public abstract class Output
	{
		internal void Query_Check(object? input)
		{
			try
			{
				Query_DataInput(input);
			}
			catch (HttpException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new HttpException(ex.Message, 400);
			}
		}
		internal abstract void Query_DataInput(object? input);
	}
}
