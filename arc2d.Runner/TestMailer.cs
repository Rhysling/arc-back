using arc2d.Mailer;
using arc2d.Models;
using arc2d.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace arc2d.Runner
{
	public  static class TestMailer
	{

		public static async Task SendTestMessage()
		{
			var msg = new ContactMessage {
				Name = "Bob Tester",
				Email = "bob@testco.com",
				Company = "Test Co",
				Phone = null,
				Message = "Here's my message" };

			var mt = new MailgunTarget(
				AppSettings.MailgunFromDomain,
				AppSettings.MailgunAuthValue,
				"noreply@american-research-capital.net",
				"rpkummer@hotmail.com,rkummer@polson.com",
				false
			);


			await mt.SendAsync(msg);
		}

	}
}
