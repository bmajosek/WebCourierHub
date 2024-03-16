using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebCourierHub.Support.ApiConfig;

namespace WebCourierHub.Controllers
{
	[Authorize]
	[Route("Offers")]
	public class OffersController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
		private readonly IApiConfig _internalApiConfig;

		public OffersController(IHttpClientFactory httpClientFactory, [FromKeyedServices("Internal")] IApiConfig internalApiConfig)
		{
			_httpClientFactory = httpClientFactory;
			_internalApiConfig = internalApiConfig;
		}

		public async Task<IActionResult> Index()
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.GetAsync($"{_internalApiConfig.Url}/api/deliveries");

			if (response.IsSuccessStatusCode)
			{
				var offersContent = await response.Content.ReadAsStringAsync();
				var offers = JsonConvert.DeserializeObject<List<WebCourierApi.Model.DTO.DeliveryDTO>>(offersContent);
				return View(offers);
			}
			return View(new List<WebCourierApi.Model.DTO.DeliveryDTO>());
		}

		[Route("{id}")]
		public async Task<IActionResult> Details(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.GetAsync($"{_internalApiConfig.Url}/api/deliveries/{id}");

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return View(offer);
			}
			return View(null);
		}

		[HttpPatch("{id}/accept")]
		public async Task<IActionResult> Accept(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.PatchAsync($"{_internalApiConfig.Url}/api/deliveries/{id}/accept", null);

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return Ok(offer);
			}
			return BadRequest(response);
		}

		[HttpPatch("{id}/reject")]
		public async Task<IActionResult> Reject(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.PatchAsync($"{_internalApiConfig.Url}/api/deliveries/{id}/reject", null);

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return Ok(offer);
			}
			return BadRequest(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.DeleteAsync($"{_internalApiConfig.Url}/api/deliveries/{id}");

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return Ok(offer);
			}
			return BadRequest(response);
		}
	}
}