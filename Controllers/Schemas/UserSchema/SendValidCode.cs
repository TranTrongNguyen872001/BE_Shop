using BE_Shop.Data;
using BE_Shop.Data.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BE_Shop.Controllers
{
	public class SendValidCode
	{
		internal Guid UserId { get; set; }
		internal IEmailService EmailService { get; set; }
	}
	public class OutputSendValidCode : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			SendValidCode input = (SendValidCode)ip;
			int ValidCode = new Random().Next(0, 1000000);
			using (var db = new DatabaseConnection())
			{
				var user = db._User.Find(input.UserId) ?? throw new HttpException(string.Empty, 404);
				user.ValidCode = ValidCode;
				db.SaveChanges();
				//var mail = new MimeMessage();
				//mail.From.Add(MailboxAddress.Parse("shopmisaproject@gmail.com"));
				//mail.To.Add(MailboxAddress.Parse(user.UserName));
				//mail.Subject = "[Tin nhắn tự động] Mã xác thực";
				//mail.Body = new TextPart(TextFormat.Html) { Text = ValidCode.ToString("000000") + " là mã xác thực của quý khách" };

				//using (var smtp =  new SmtpClient())
				//{
				//	smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
				//	smtp.Authenticate("shopmisaproject@gmail.com", "ohtfugaymvcgsmzw");
				//	smtp.Send(mail);
				//	smtp.Dispose();
				//}
				input.EmailService.SendMail(new MailManager()
				{
					To = user.UserName,
					Subject = "[Tin nhắn tự động] Mã xác thực",
					Body = ValidCode.ToString("000000") + " là mã xác thực của quý khách"
				});
			}
		}
	}
	public class ValidUser
	{
		public int ValidCode { get; set; }
		public Guid UserId { get; set; }
		internal IEmailService EmailService { get; set; }
	}
	public class OutputValidUser : Output
	{
		internal override void Query_DataInput(object? ip)
		{
			var input = (ValidUser)ip;
			using (var db = new DatabaseConnection())
			{
				var user = db._User.Find(input.UserId) ?? throw new HttpException(string.Empty, 404);
				if(input.ValidCode != user.ValidCode)
				{
					throw new HttpException(string.Empty, 400);
				}
				var TempPassword = Converter.RamdomByte(20);
				var TempPasswordMD5 = Converter.MD5Convert(TempPassword);
				//var mail = new MimeMessage();
				//mail.From.Add(MailboxAddress.Parse("shopmisaproject@gmail.com"));
				//mail.To.Add(MailboxAddress.Parse(user.UserName));
				//mail.Subject = "[Tin nhắn tự động] Tạo mật khẩu mới";
				//mail.Body = new TextPart(TextFormat.Html) { Text = TempPassword + " là mật khẩu mới của quý khách, quý khách vui lòng đổi mật khẩu trước khi sử dụng" };

				//using (var smtp = new SmtpClient())
				//{
				//	smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
				//	smtp.Authenticate("shopmisaproject@gmail.com", "ohtfugaymvcgsmzw");
				//	smtp.Send(mail);
				//	smtp.Dispose();
				//}
				input.EmailService.SendMail(new MailManager()
				{
					To = user.UserName,
					Subject = "[Tin nhắn tự động] Tạo mật khẩu mới",
					Body = TempPassword + " là mật khẩu mới của quý khách, quý khách vui lòng đổi mật khẩu trước khi sử dụng"
				});
				user.ValidCode = null;
				user.Role = "Member";
				user.Password = TempPasswordMD5;
				db.SaveChanges();
			}
		}
	}
}
