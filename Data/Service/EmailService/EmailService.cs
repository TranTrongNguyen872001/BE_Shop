using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace BE_Shop.Data.Service
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration conf;

		public EmailService(IConfiguration conf)
		{
			this.conf = conf;
		}
		public void SendMail(MailManager email)
		{
			var mail = new MimeMessage();
			mail.From.Add(MailboxAddress.Parse(conf.GetSection("EmailUsername").Value));
			mail.To.Add(MailboxAddress.Parse(email.To));
			mail.Subject = email.Subject;
			mail.Body = new TextPart(TextFormat.Html) { Text = email.Body };

			using (var smtp = new SmtpClient())
			{
				smtp.Connect(conf.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
				smtp.Authenticate(conf.GetSection("EmailUsername").Value, conf.GetSection("EmailPassword").Value);
				smtp.Send(mail);
				smtp.Dispose();
			}
		}
	}
}
