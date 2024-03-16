using Microsoft.EntityFrameworkCore;
using WebCourierApi.Data;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private readonly WebCourierApiDbContext _context;

        public NewsletterRepository(WebCourierApiDbContext context)
        {
            _context = context;
        }

        public async Task AddNewsletterAsync(string email)
        {
            var newsletterPOCO = new NewsletterPOCO { Mail = email };
            await _context.Newsletters.AddAsync(newsletterPOCO);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Newsletter>> GetAllSubscribersAsync()
        {
            return await _context.Newsletters.Select(x => new Newsletter() { Mail = x.Mail, Id = x.Id }).ToListAsync();
        }

        public async Task<Newsletter?> GetSubscriberByEmailAsync(string email)
        {
            return await _context.Newsletters.Select(x => new Newsletter() { Mail = x.Mail, Id = x.Id }).FirstOrDefaultAsync(x => x.Mail == email);
        }

        public async Task RemoveSubscriberAsync(Newsletter subscriber)
        {
            _context.Newsletters.Remove(new NewsletterPOCO() { Id = subscriber.Id, Mail = subscriber.Mail });
            await _context.SaveChangesAsync();
        }
    }
}