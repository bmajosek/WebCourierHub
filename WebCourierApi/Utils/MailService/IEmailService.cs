namespace WebCourierApi.Utils.MailService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string message);

        public Task SendWeeklyEmails(string toEmail, string subject, string message);
    }
}