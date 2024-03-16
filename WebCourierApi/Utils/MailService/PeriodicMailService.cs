using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Utils.MailService;

namespace WebCourierApi.Utils.MailService
{
    public class PeriodicMailService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _timeInterval;
        private readonly string _url;

        public PeriodicMailService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _url = configuration["InternalApi:Url"]!;
            if (int.TryParse(configuration["SendGrid:PeriodicIntervalDays"], out int intervalDays))
            {
                _timeInterval = TimeSpan.FromDays(intervalDays);
            }
            else
            {
                _timeInterval = TimeSpan.FromDays(7); // Default to 7 days if configuration is not set or invalid
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var newsletterRepository = scope.ServiceProvider.GetRequiredService<INewsletterRepository>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var subscribers = await newsletterRepository.GetAllSubscribersAsync();
                    foreach (var subscriber in subscribers)
                    {
                        // Replace these parameters with your actual email subject and content
                        var message = "Here is your weekly newsletter content!";
                        message += $"<p>Click <a href='{_url}/api/Newsletter/unsubscribe/{subscriber.Mail}'>here</a> to unsubscribe from our newsletter.</p>";
                        await emailService.SendEmailAsync(subscriber.Mail, "Weekly Newsletter", message);
                    }
                }

                await Task.Delay(_timeInterval, stoppingToken);
            }
        }
    }
}