using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCourierApi.Data.Seeding;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Configuration
{
    public class CurrencyPOCOConfiguration : IEntityTypeConfiguration<CurrencyPOCO>
    {
        private readonly ISeeder<CurrencyPOCO> _seeder;

        public CurrencyPOCOConfiguration(ISeeder<CurrencyPOCO> seeder)
        {
            _seeder = seeder;
        }

        public void Configure(EntityTypeBuilder<CurrencyPOCO> currencyEntity)
        {
            currencyEntity.HasKey(currency => currency.Id);
            currencyEntity.Property(currency => currency.Id).ValueGeneratedNever();
            currencyEntity.HasMany(currency => currency.Countries)
                .WithOne(country => country.Currency);
            currencyEntity.HasMany(currency => currency.Deliveries)
                .WithOne(delivery => delivery.PricingCurrency);
            currencyEntity.Property(currency => currency.ShortName).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            currencyEntity.HasData(_seeder.Seeds);
        }
    }
}
