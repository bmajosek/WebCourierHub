using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCourierApi.Data.Seeding;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Configuration
{
    public class CountryPOCOConfiguration : IEntityTypeConfiguration<CountryPOCO>
    {
        private readonly ISeeder<CountryPOCO> _seeder;

        public CountryPOCOConfiguration(ISeeder<CountryPOCO> seeder)
        {
            _seeder = seeder;
        }

        public void Configure(EntityTypeBuilder<CountryPOCO> countryEntity)
        {
            countryEntity.HasKey(country => country.Id);
            countryEntity.Property(country => country.Id).ValueGeneratedNever();
            countryEntity.Property(country => country.Name).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            countryEntity.HasOne(country => country.Currency)
                .WithMany(currency => currency.Countries)
                .HasForeignKey(country => country.CurrencyId)
                .IsRequired();
            countryEntity.HasMany(country => country.PickupInquires)
                .WithOne(inquire => inquire.PickupCountry);
            countryEntity.HasMany(country => country.DeliveryInquires)
                .WithOne(inquire => inquire.DeliveryCountry);

            countryEntity.HasData(_seeder.Seeds);
        }
    }
}
