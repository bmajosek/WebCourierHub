using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Configuration
{
    public class DeliveryPOCOConfiguration : IEntityTypeConfiguration<DeliveryPOCO>
    {
        public void Configure(EntityTypeBuilder<DeliveryPOCO> deliveryEntity)
        {
            deliveryEntity.HasKey(delivery => delivery.Id);
            deliveryEntity.HasOne(delivery => delivery.Inquire)
                .WithOne(inquire => inquire.DeliveryRequest)
                .HasForeignKey<DeliveryPOCO>(delivery => delivery.InquireId)
                .IsRequired();
            deliveryEntity.HasOne(delivery => delivery.PricingCurrency)
                .WithMany(currency => currency.Deliveries)
                .HasForeignKey(delivery => delivery.PricingCurrencyId)
                .IsRequired();
            deliveryEntity.Navigation(delivery => delivery.PricingCurrency).AutoInclude();
            deliveryEntity.OwnsOne(delivery => delivery.Process, processEntity =>
            {
                processEntity.ToTable("Processes");
                processEntity.WithOwner(process => process.DeliveryRequest)
                    .HasForeignKey(process => process.DeliveryRequestId);
                processEntity.Property(process => process.IsDelivered).IsRequired().HasColumnType(ConfigurationConstants.BIT);
                processEntity.Property(process => process.PickupCourierName).HasMaxLength(ConfigurationConstants.NormalTextLength);
                processEntity.Property(process => process.DeliveryCourierName).HasMaxLength(ConfigurationConstants.NormalTextLength);
                processEntity.Property(process => process.PickupTimestamp).HasColumnType(ConfigurationConstants.DATETIME);
                processEntity.Property(process => process.DeliveryTimestamp).HasColumnType(ConfigurationConstants.DATETIME);
                processEntity.Property(process => process.Notes).HasMaxLength(ConfigurationConstants.LongTextLength);
            });
            deliveryEntity.Property(delivery => delivery.CreationDate).IsRequired().HasColumnType(ConfigurationConstants.DATETIME);
            deliveryEntity.Property(delivery => delivery.ModificationDate).IsRequired().HasColumnType(ConfigurationConstants.DATETIME);
            deliveryEntity.Property(offer => offer.PricingBase).IsRequired().HasColumnType(ConfigurationConstants.MONEY);
            deliveryEntity.Property(offer => offer.PricingTaxes).IsRequired().HasColumnType(ConfigurationConstants.MONEY);
            deliveryEntity.Property(offer => offer.PricingFees).IsRequired().HasColumnType(ConfigurationConstants.MONEY);
            deliveryEntity.ComplexProperty(delivery => delivery.Client, clientEntity => {
                clientEntity.Property(client => client.EmailAddress).IsRequired().HasMaxLength(ConfigurationConstants.NormalTextLength);
                clientEntity.Property(client => client.FirstName).HasMaxLength(ConfigurationConstants.NormalTextLength);
                clientEntity.Property(client => client.LastName).HasMaxLength(ConfigurationConstants.NormalTextLength);
                clientEntity.Property(client => client.CompanyName).HasMaxLength(ConfigurationConstants.NormalTextLength);
            });
            deliveryEntity.Property(delivery => delivery.IsPending).IsRequired().HasColumnType(ConfigurationConstants.BIT);
        }
    }
}
