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
		public DbSet<FileManager> _FileManager { get; set; }
		public DbSet<Comment> _Comment { get; set; }
        public DbSet<ProductCategory> _ProductCategory { get; set; }
        public DbSet<ChatLine> _ChatLine { get; set; }
        public DbSet<Discount> _Discount { get; set; }
        public DbSet<Room> _Rooms { get; set; }
        public DbSet<Message> _Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(
				//@"Data Source=KAITOKIDS872001;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
				@"Data Source=112.78.2.40,1433;Initial Catalog=dre56342_Shop;User ID=dre56342_Admin;Password=Kaitokids872001");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderDetail>()
				.HasKey(nameof(OrderDetail.ProductId), nameof(OrderDetail.OrderId));
			modelBuilder.Entity<Comment>()
				.HasKey(nameof(Comment.TT), nameof(Comment.ProductId));
		}
		//xóa database và tạo lại :) đỡ tốn thời gian :( mệt quá!
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
		static public string RamdomByte(int i)
		{
			var randomNumber = new byte[i];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
	}
	public abstract class Output
	{
		internal abstract void Query_DataInput(object? ip);
	}
}
