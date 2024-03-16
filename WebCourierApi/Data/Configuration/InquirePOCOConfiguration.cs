using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Configuration
{
    public class InquirePOCOConfiguration : IEntityTypeConfiguration<InquirePOCO>
    {
        public void Configure(EntityTypeBuilder<InquirePOCO> inquireEntity)
        {
            inquireEntity.HasKey(inquire => inquire.Id);
            inquireEntity.HasOne(inquire => inquire.DeliveryRequest)
                .WithOne(delivery => delivery.Inquire);
            inquireEntity.HasOne(inquire => inquire.PickupCountry)
                .WithMany(country => country.PickupInquires)
                .HasForeignKey(inquire => inquire.PickupCountryId)
                .IsRequired();
            inquireEntity.Navigation(inquire => inquire.PickupCountry).AutoInclude();
            inquireEntity.HasOne(inquire => inquire.DeliveryCountry)
                .WithMany(country => country.DeliveryInquires)
                .HasForeignKey(inquire => inquire.DeliveryCountryId)
                .IsRequired();
            inquireEntity.Navigation(inquire => inquire.DeliveryCountry).AutoInclude();
            inquireEntity.Property(inquire => inquire.CreationDate).IsRequired().HasColumnType(ConfigurationConstants.DATETIME);
            inquireEntity.ComplexProperty(inquire => inquire.Package).IsRequired();
            inquireEntity.Property(inquire => inquire.PickupDate).IsRequired().HasColumnType(ConfigurationConstants.DATETIME);
            inquireEntity.Property(inquire => inquire.PickupZipCode).IsRequired().HasMaxLength(6);
            inquireEntity.Property(inquire => inquire.PickupTown).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            inquireEntity.Property(inquire => inquire.PickupStreet).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            inquireEntity.Property(inquire => inquire.PickupBuildingNumber).IsRequired();
            inquireEntity.Property(inquire => inquire.PickupApartmentNumber);
            inquireEntity.Property(inquire => inquire.DeliveryDate).IsRequired().HasColumnType(ConfigurationConstants.DATETIME);
            inquireEntity.Property(inquire => inquire.DeliveryZipCode).IsRequired().HasMaxLength(6);
            inquireEntity.Property(inquire => inquire.DeliveryTown).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            inquireEntity.Property(inquire => inquire.DeliveryStreet).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            inquireEntity.Property(inquire => inquire.DeliveryBuildingNumber).IsRequired();
            inquireEntity.Property(inquire => inquire.DeliveryApartmentNumber);
            inquireEntity.ComplexProperty(inquire => inquire.DeliveryOptions, optionsEntity => {
                optionsEntity.Property(options => options.HighPriority).IsRequired().HasColumnType(ConfigurationConstants.BIT);
                optionsEntity.Property(options => options.IsForCompany).IsRequired().HasColumnType(ConfigurationConstants.BIT);
                optionsEntity.Property(options => options.WeekendDelivery).IsRequired().HasColumnType(ConfigurationConstants.BIT);
            });
            inquireEntity.Property(inquire => inquire.OwnerKey).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
            inquireEntity.OwnsMany(inquire => inquire.Offers, offerEntity =>
            {
                offerEntity.ToTable("Offers");
                offerEntity.HasKey(offer => new { offer.OfferNumber, offer.InquireId });
                offerEntity.Property(offer => offer.OfferNumber).ValueGeneratedNever();
                offerEntity.WithOwner(offer => offer.Inquire)
                    .HasForeignKey(offer => offer.InquireId);
                offerEntity.HasOne(offer => offer.PricingCurrency)
                    .WithMany()
                    .HasForeignKey(offer => offer.PricingCurrencyId);
                offerEntity.Navigation(offer => offer.PricingCurrency).AutoInclude();
                offerEntity.Property(offer => offer.ValidTo).IsRequired().HasColumnType(ConfigurationConstants.DATETIME);
                offerEntity.Property(offer => offer.PricingBase).IsRequired().HasColumnType(ConfigurationConstants.MONEY);
                offerEntity.Property(offer => offer.PricingTaxes).IsRequired().HasColumnType(ConfigurationConstants.MONEY);
                offerEntity.Property(offer => offer.PricingFees).IsRequired().HasColumnType(ConfigurationConstants.MONEY);
            });
        }
    }
}
