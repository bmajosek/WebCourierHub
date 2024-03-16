using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebCourierApi.Model.DTO;
using WebCourierHub.Support.ApiConfig;
using Microsoft.Extensions.Configuration;

namespace WebCourierHub.Controllers
{
	[Authorize]
	[Route("Deliveries")]
	public class DeliveriesController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IApiConfig _internalApiConfig;
		private readonly IConfiguration _configuration;

		public DeliveriesController(IHttpClientFactory httpClientFactory, [FromKeyedServices("Internal")] IApiConfig internalApiConfig, IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
			_internalApiConfig = internalApiConfig;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index()
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.GetAsync($"{_internalApiConfig.Url}/api/deliveries?RequestStatus=Accepted");

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

		[HttpPatch("{id}/pickup")]
		public async Task<IActionResult> Pickup(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.PatchAsync($"{_internalApiConfig.Url}/api/deliveries/{id}/pickup?courierName={User.Identity?.Name ?? "Unknown"}&pickupTime={DateTime.Now:s}", null);

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return Ok(offer);
			}
			return BadRequest(response);
		}

		[HttpPatch("{id}/fulfil")]
		public async Task<IActionResult> Fulfil(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.PatchAsync($"{_internalApiConfig.Url}/api/deliveries/{id}/fulfil?courierName={User.Identity?.Name ?? "Unknown"}&deliveryTime={DateTime.Now:s}", null);

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return Ok(offer);
			}
			return BadRequest(response);
		}

		[HttpPatch("{id}/giveup")]
		public async Task<IActionResult> Giveup(int id, string reason)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.PatchAsync($"{_internalApiConfig.Url}/api/deliveries/{id}/giveup?courierName={User.Identity?.Name ?? "Unknown"}&deliveryTime={DateTime.Now:s}&reason={reason}", null);

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				var offer = JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
				return Ok(offer);
			}
			return BadRequest(response);
		}

		[HttpPost("{id}/generate-receipt")]
		public async Task<IActionResult> GenerateReceipt(int id)
		{
			var delivery = await GetDeliveryDetails(id);

			// Generate the receipt content based on the delivery information
			string receiptContent = GenerateReceiptContent(delivery);

			// Save the receipt to Azure Blob Storage
			string blobName = $"receipt_{id}.txt";
			await SaveReceiptToBlobStorage(blobName, receiptContent);

			// Provide a link to download the receipt
			string blobUrl = GetBlobUrl(blobName);

			return Json(new { receiptUrl = blobUrl });
		}

		private string GenerateReceiptContent(DeliveryDTO delivery)
		{
			StringBuilder receiptBuilder = new StringBuilder();

			receiptBuilder.AppendLine($"Delivery Receipt");
			receiptBuilder.AppendLine($"-----------------");
			receiptBuilder.AppendLine($"Delivery ID: {delivery.Id}");
			receiptBuilder.AppendLine($"Creation Date: {delivery.CreationDate}");
			receiptBuilder.AppendLine($"Modification Date: {delivery.ModificationDate}");
			receiptBuilder.AppendLine();

			// Inquire Details
			receiptBuilder.AppendLine($"Inquire Details");
			receiptBuilder.AppendLine($"-----------------");
			receiptBuilder.AppendLine($"Package Dimensions: {delivery.Inquire?.Package.WidthCM} x {delivery.Inquire?.Package.LengthCM} x {delivery.Inquire?.Package.HeightCM} cm");
			receiptBuilder.AppendLine($"Package Weight: {delivery.Inquire?.Package.WeightKG} kg");
			receiptBuilder.AppendLine($"Pickup Date: {delivery.Inquire?.PickupDate}");
			// Add more inquire details as needed
			receiptBuilder.AppendLine();

			// Pricing Details
			receiptBuilder.AppendLine($"Pricing Details");
			receiptBuilder.AppendLine($"-----------------");
			receiptBuilder.AppendLine($"Base Cost: {delivery.Pricing.Base} {delivery.Pricing.Currency}");
			receiptBuilder.AppendLine($"Taxes: {delivery.Pricing.Taxes} {delivery.Pricing.Currency}");
			receiptBuilder.AppendLine($"Fees: {delivery.Pricing.Fees} {delivery.Pricing.Currency}");
			receiptBuilder.AppendLine($"Total Cost: {delivery.Pricing.Total} {delivery.Pricing.Currency}");
			receiptBuilder.AppendLine();

			// Client Details
			receiptBuilder.AppendLine($"Client Details");
			receiptBuilder.AppendLine($"-----------------");
			receiptBuilder.AppendLine($"Client Email: {delivery.Client.EmailAddress}");
			// Add more client details as needed
			receiptBuilder.AppendLine();

			// Request Status
			receiptBuilder.AppendLine($"Request Status: {delivery.RequestStatus}");
			receiptBuilder.AppendLine();

			// Delivery Process Details
			if (delivery.Process != null)
			{
				receiptBuilder.AppendLine($"Delivery Process");
				receiptBuilder.AppendLine($"-----------------");
				receiptBuilder.AppendLine($"Status: {delivery.Process.DeliveryStatus}");

				if (delivery.Process.DeliveryStatus == "Started")
				{
					receiptBuilder.AppendLine($"Package has not been picked up yet.");
				}
				else
				{
					receiptBuilder.AppendLine($"{delivery.Process.PickupCourierName} picked up the package ({delivery.Process.PickupTimestamp}).");
				}

				if (delivery.Process.DeliveryStatus == "Delivered")
				{
					receiptBuilder.AppendLine($"{delivery.Process.DeliveryCourierName} delivered the package ({delivery.Process.DeliveryTimestamp}).");
				}

				if (delivery.Process.DeliveryStatus == "CannotDeliver")
				{
					receiptBuilder.AppendLine($"{delivery.Process.DeliveryCourierName} was unable to deliver the package ({delivery.Process.DeliveryTimestamp}).");
					receiptBuilder.AppendLine($"Reason: {delivery.Process.Notes}");
				}

				// Add more delivery process details as needed
			}

			return receiptBuilder.ToString();
		}

		private async Task SaveReceiptToBlobStorage(string blobName, string content)
		{
			// Retrieve the Azure Blob Storage connection string and container name from configuration
			string connectionString = _configuration["AzureBlobStorage:ConnectionString"];
			string containerName = _configuration["AzureBlobStorage:ContainerName"];

			// Create a BlobServiceClient
			var blobServiceClient = new BlobServiceClient(connectionString);

			// Get a reference to the container
			var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

			// Get a reference to the blob
			var blobClient = containerClient.GetBlobClient(blobName);

			// Upload the content to the blob
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
			{
				await blobClient.UploadAsync(stream, true);
			}
		}

		private string GetBlobUrl(string blobName)
		{
			// Retrieve the Azure Blob Storage connection string and container name from configuration
			string connectionString = _configuration["AzureBlobStorage:ConnectionString"];
			string containerName = _configuration["AzureBlobStorage:ContainerName"];

			// Create a BlobServiceClient
			var blobServiceClient = new BlobServiceClient(connectionString);

			// Get a reference to the container
			var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

			// Get a reference to the blob
			var blobClient = containerClient.GetBlobClient(blobName);

			// Return the URL of the blob
			return blobClient.Uri.ToString();
		}

		private async Task<WebCourierApi.Model.DTO.DeliveryDTO> GetDeliveryDetails(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			_internalApiConfig.AddCredentialsTo(httpClient);

			var response = await httpClient.GetAsync($"{_internalApiConfig.Url}/api/deliveries/{id}");

			if (response.IsSuccessStatusCode)
			{
				var offerContent = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<WebCourierApi.Model.DTO.DeliveryDTO>(offerContent);
			}

			return null;
		}
	}
}