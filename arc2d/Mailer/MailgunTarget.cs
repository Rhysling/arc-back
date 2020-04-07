using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using arc2d.Models;

namespace arc2d.Mailer
{
	public class MailgunTarget
	{
		private HttpClient client;
		private const string baseAddress = "https://api.mailgun.net/v3/{0}/messages";
		private string fromAddress;
		private string toAddresses;
		private bool isProduction;

		public MailgunTarget(
			string domain,
			string authValue,
			string fromAddress,
			string toAddresses, // ',' delimited
			bool isProduction = true
		)
		{
			client = new HttpClient();
			client.BaseAddress = new Uri(String.Format(baseAddress, domain));
			client.DefaultRequestHeaders.Add("Authorization", authValue);

			this.fromAddress = fromAddress;
			this.toAddresses = toAddresses;
			this.isProduction = isProduction;
		}

		public async Task<HttpResponseMessage> SendAsync(ContactMessage item)
		{

			var msg = new MailMessage(item, toAddresses);

			var parameters = new Dictionary<string, string> {
				{ "from", fromAddress },
				{ "to", toAddresses },
				{ "subject", msg.Subject },
				{ "text", $"Contact name: {item.Name};\r\n Email: {item.Email};\r\n Company: {item.Company};\r\n Phone: {(string.IsNullOrWhiteSpace(item.Phone) ? "None" : item.Message)};\r\n Message: {(string.IsNullOrWhiteSpace(item.Message) ? "None" : item.Message)}" },
				{ "html", msg.RebderBody() }
			};

			var encodedContent = new FormUrlEncodedContent(parameters);

			HttpResponseMessage res;
			using (res = await client.PostAsync("", encodedContent).ConfigureAwait(false));

			return res;
		}

	}
}