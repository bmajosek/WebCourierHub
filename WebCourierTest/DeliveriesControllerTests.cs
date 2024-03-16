using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using Newtonsoft.Json;
using WebCourierApi.Model.DTO;
using WebCourierApi;
using WebCourierApi.Utils.DynamicQuery.Queries;
using System.Net;

// Additional using statements for your testing framework

namespace WebCourierTest
{
    public class DeliveriesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly string _externalApiKey = "external-api-key"; // Replace with external API key
        private readonly string _internalApiKey = "internal-api-key"; // Replace with internal API key

        public DeliveriesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetDelivery_ReturnsDeliveryDetails()
        {
            // Arrange
            int testDeliveryId = 1; // Use a known ID from your test database
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Act
            var response = await _client.GetAsync($"/api/Deliveries/{testDeliveryId}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var delivery = JsonConvert.DeserializeObject<DeliveryDTO>(responseString);

            // Assert
            Assert.NotNull(delivery);
            Assert.Equal(testDeliveryId, delivery.Id);
            // Additional assertions as needed
        }

        [Fact]
        public async Task GetDeliveries_ReturnsListOfDeliveries()
        {
            // Arrange - assuming a valid query
            var query = new DeliveryDynamicQuery(); // Set your query parameters as needed
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Act
            var response = await _client.GetAsync("/api/Deliveries?" + query.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var deliveries = JsonConvert.DeserializeObject<IEnumerable<DeliveryDTO>>(responseString);

            // Assert
            Assert.NotNull(deliveries);
            Assert.NotEmpty(deliveries);
        }

        [Fact]
        public async Task DeleteDelivery_SuccessfulDeletion()
        {
            // Arrange
            int testDeliveryId = 1; // Use a known deletable ID from your test database
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Act
            var response = await _client.DeleteAsync($"/api/Deliveries/{testDeliveryId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}