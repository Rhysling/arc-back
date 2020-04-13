using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using arc2d.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace arc2d.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecaptchaController : ControllerBase
	{
		private readonly IHttpClientFactory _clientFactory;

		public RecaptchaController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] RecaptchaToken token)
		{
			var url = "https://www.google.com/recaptcha/api/siteverify";
			var parameters = new Dictionary<string, string> { 
				{
					"secret",
					"6Lewq88UAAAAABpbTvA9QDOD7zl_BhPC2eaPC_FM"
				}, {
					"response", 
					token.Token 
				}
			};
			var encodedContent = new FormUrlEncodedContent(parameters);

			var client = _clientFactory.CreateClient();

			var response = await client.PostAsync(url, encodedContent);
			string responseContent = null;
			if (response.StatusCode == HttpStatusCode.OK)
			{
				responseContent = await response.Content.ReadAsStringAsync();
				return Ok(responseContent);
			}

			return BadRequest(new { Success = false, Content = responseContent ?? "None" });
		}

	}
}