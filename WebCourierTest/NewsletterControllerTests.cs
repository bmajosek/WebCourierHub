using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using WebCourierHub.Classes;

namespace WebCourierTest
{
    public class NewsletterApiIntegrationTest
    {
        private readonly HttpClient _client;

        public NewsletterApiIntegrationTest()
        {
            _client = new HttpClient();
        }

        [Fact]
        public async Task AddSubscriber_ForwardsToBackendService()
        {
            var mailDto = new MailDto { Mail = "test@example.com" };
            var json = JsonConvert.SerializeObject(mailDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7032/api/Newsletter/Add", content);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}