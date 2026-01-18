using arc2d.Mailer;
using arc2d.Models;
using arc2d.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace arc2d.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SendContactController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] ContactMessage msg, [FromQuery] String k)
		{
			if (k != "812g")
				return StatusCode(403);

			string tos = AppSettings.IsProduction ? "bob@american-research-capital.com,luke@american-research-capital.com" : "rpkummer@hotmail.com,rkummer@polson.com";

			var mt = new MailgunTarget(
				AppSettings.MailgunFromDomain,
				AppSettings.MailgunAuthValue,
				"noreply@american-research-capital.net",
				tos
			);

			var res = await mt.SendAsync(msg);

			return Ok(new { IsSuccess = res.IsSuccessStatusCode, Message = res.IsSuccessStatusCode ? "Done" : res.ReasonPhrase });
		}

	}
}
