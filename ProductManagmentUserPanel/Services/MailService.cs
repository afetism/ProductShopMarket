using System.Net.Mail;
using System.Net;

namespace ProductManagmentUserPanel.Services;

public class MailService
{
	public void SendMail(string to, string subject, string body, bool isHtml = false)
	{
		var smtp = new SmtpClient("smtp.gmail.com")
		{
			Port = 587,
			Credentials = new NetworkCredential("mirtalibemirli498@gmail.com", "aytndmgzqcukvmds"),
			EnableSsl = true,
		};

		var mailMessage = new MailMessage()
		{
			Subject = subject,
			Body = body,
			From = new MailAddress("mirtalibemirli498@gmail.com"),
			IsBodyHtml = isHtml
		};

		mailMessage.To.Add(to);

		try
		{
			smtp.Send(mailMessage);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
