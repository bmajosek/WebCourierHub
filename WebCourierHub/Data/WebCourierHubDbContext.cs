using Microsoft.EntityFrameworkCore;
using WebCourierHub.Models;

namespace WebCourierHub.Data
{
    public class WebCourierHubDbContext : DbContext
    {
        public WebCourierHubDbContext(DbContextOptions<WebCourierHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Inquiry> Inquiry { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Delivery> Delivery { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}