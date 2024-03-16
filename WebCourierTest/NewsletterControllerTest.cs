using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebCourierApi.Controllers;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Model.Entities;
using WebCourierApi.Utils.MailService;
using WebCourierHub.Classes;
using Xunit;

namespace WebCourierTest
{
    public class NewsletterControllerTests
    {
        private readonly Mock<IEmailService> mockEmailService;
        private readonly Mock<INewsletterRepository> mockNewsletterRepository;
        private readonly NewsletterController controller;

        public NewsletterControllerTests()
        {
            mockEmailService = new Mock<IEmailService>();
            mockNewsletterRepository = new Mock<INewsletterRepository>();
            controller = new NewsletterController(mockEmailService.Object, mockNewsletterRepository.Object);
        }

        [Fact]
        public async Task Add_ValidEmail_ReturnsOkResult()
        {
            var mailDto = new MailDto { Mail = "test@example.com" };
            var result = await controller.Add(mailDto);

            Assert.IsType<OkObjectResult>(result);
            mockNewsletterRepository.Verify(repo => repo.AddNewsletterAsync(It.IsAny<string>()), Times.Once);
            mockEmailService.Verify(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Add_InvalidEmail_ReturnsBadRequest()
        {
            var mailDto = new MailDto { Mail = "invalid" };
            var result = await controller.Add(mailDto);

            Assert.IsType<BadRequestObjectResult>(result);
            mockNewsletterRepository.Verify(repo => repo.AddNewsletterAsync(It.IsAny<string>()), Times.Never);
            mockEmailService.Verify(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Unsubscribe_ExistingEmail_ReturnsOkResult()
        {
            string email = "test@example.com";
            mockNewsletterRepository.Setup(repo => repo.GetSubscriberByEmailAsync(email)).ReturnsAsync(new Newsletter { Mail = email });

            var result = await controller.Unsubscribe(email);

            Assert.IsType<OkObjectResult>(result);
            mockNewsletterRepository.Verify(repo => repo.RemoveSubscriberAsync(It.IsAny<Newsletter>()), Times.Once);
            mockEmailService.Verify(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}