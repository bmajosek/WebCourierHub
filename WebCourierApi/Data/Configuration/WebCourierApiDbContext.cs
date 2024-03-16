using Microsoft.EntityFrameworkCore;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data
{
    public class WebCourierApiDbContext : DbContext
    {
        private readonly IEntityTypeConfiguration<InquirePOCO> _inquireConfiguration;
        private readonly IEntityTypeConfiguration<DeliveryPOCO> _deliveryConfiguration;
        private readonly IEntityTypeConfiguration<CountryPOCO> _countryConfiguration;
        private readonly IEntityTypeConfiguration<CurrencyPOCO> _currencyConfiguration;
        private readonly IEntityTypeConfiguration<NewsletterPOCO> _newsletterConfiguration;

        public WebCourierApiDbContext(
            DbContextOptions<WebCourierApiDbContext> options,
            IEntityTypeConfiguration<InquirePOCO> inquireConfiguration,
            IEntityTypeConfiguration<DeliveryPOCO> deliveryConfiguration,
            IEntityTypeConfiguration<CountryPOCO> countryConfiguration,
            IEntityTypeConfiguration<CurrencyPOCO> currencyConfiguration,
            IEntityTypeConfiguration<NewsletterPOCO> newsletterConfiguration
        ) : base(options)
        {
            _inquireConfiguration = inquireConfiguration;
            _deliveryConfiguration = deliveryConfiguration;
            _countryConfiguration = countryConfiguration;
            _currencyConfiguration = currencyConfiguration;
            _newsletterConfiguration = newsletterConfiguration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(_inquireConfiguration);
            modelBuilder.ApplyConfiguration(_deliveryConfiguration);
            modelBuilder.ApplyConfiguration(_countryConfiguration);
            modelBuilder.ApplyConfiguration(_currencyConfiguration);
            modelBuilder.ApplyConfiguration(_newsletterConfiguration);
        }

        public DbSet<InquirePOCO> Inquiries { get; set; }
        public DbSet<DeliveryPOCO> Deliveries { get; set; }
        public DbSet<CurrencyPOCO> Currencies { get; set; }
        public DbSet<CountryPOCO> Countries { get; set; }
        public DbSet<NewsletterPOCO> Newsletters { get; set; }
    }
}