using WebCourierApi.Model.Entities;

namespace WebCourierApi.Data.Repositories
{
    public interface INewsletterRepository
    {
        Task AddNewsletterAsync(string email);

        Task<List<Newsletter>> GetAllSubscribersAsync();

        Task<Newsletter?> GetSubscriberByEmailAsync(string email);

        Task RemoveSubscriberAsync(Newsletter subscriber);
    }
}