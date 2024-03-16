using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using Newtonsoft.Json;
using WebCourierApi.Model.DTO;
using WebCourierApi;
using WebCourierApi.Utils.DynamicQuery.Queries;
using System.Net;
using System.Text;
using WebCourierHub.Support.ApiConfig;

namespace WebCourierTest
{
    public class InquiriesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly string _externalApiKey = "Tinky-Winky"; // Replace with external API key
        private readonly string _internalApiKey = "No-No"; // Replace with internal API key

        public InquiriesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetInquires_ReturnsListOfInquires()
        {
            // Arrange - assuming a valid query
            var query = new InquireDynamicQuery(); // Set your query parameters as needed
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Adjust the URL to match the InquiryApiController configuration
            var response = await _client.GetAsync($"https://localhost:7069/api/Inquiries/ListAll?" + query.ToString());
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var inquires = JsonConvert.DeserializeObject<IEnumerable<InquireDetailsDTO>>(responseString);

            // Assert
            Assert.NotNull(inquires);
            Assert.NotEmpty(inquires);
        }

        [Fact]
        public async Task GetInquire_ReturnsSpecificInquireDetails()
        {
            // Arrange
            int testInquireId = 1; // Use a known ID from your test database
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Act
            var response = await _client.GetAsync($"https://localhost:7069/api/Inquiries/{testInquireId}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var inquire = JsonConvert.DeserializeObject<InquireDetailsDTO>(responseString);

            // Assert
            Assert.NotNull(inquire);
            Assert.Equal(testInquireId, inquire.Id);
            // Additional assertions as needed
        }

        [Fact]
        public async Task PostInquire_CreatesNewInquire()
        {
            // Arrange
            var inquireDTO = new InquireCreationDTO
            {
                // Populate with test data
            };
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(inquireDTO), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7069/api/Inquiries", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var createdInquire = JsonConvert.DeserializeObject<InquireDetailsDTO>(responseString);
            Assert.NotNull(createdInquire);
            // Additional assertions as needed
        }

        [Fact]
        public async Task DeleteInquire_SuccessfulDeletion()
        {
            // Arrange
            int testInquireId = 1; // Use a known deletable ID from your test database
            _client.DefaultRequestHeaders.Add("X-Api-Key", _externalApiKey);

            // Act
            var response = await _client.DeleteAsync($"https://localhost:7069/api/Inquiries/{testInquireId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}