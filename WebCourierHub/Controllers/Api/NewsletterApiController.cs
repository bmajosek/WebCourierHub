using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebCourierHub.Classes;
using WebCourierHub.Support.ApiConfig;

namespace WebCourierHub.Controllers.Api
{
    [Route("api/Newsletter")]
    [ApiController]
    public class NewsletterApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiConfig _internalApiConfig;

        public NewsletterApiController(IHttpClientFactory httpClientFactory, [FromKeyedServices("Internal")] IApiConfig internalApiConfig)
        {
            _httpClientFactory = httpClientFactory;
            _internalApiConfig = internalApiConfig;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(MailDto mail)
        {
            var httpClient = _httpClientFactory.CreateClient();
            _internalApiConfig.AddCredentialsTo(httpClient);
            var json = JsonConvert.SerializeObject(mail);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                // Wysyłanie zapytania POST do utworzenia nowego inquiry
                var response = await httpClient.PostAsync($"{_internalApiConfig.Url}/api/Newsletter", content);
                if (response.IsSuccessStatusCode)
                {
                    var newsletterResponse = await response.Content.ReadAsStringAsync();
                    return Ok(newsletterResponse);
                }
                return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                return BadRequest($"blad {e}");
            }
        }
    }
}