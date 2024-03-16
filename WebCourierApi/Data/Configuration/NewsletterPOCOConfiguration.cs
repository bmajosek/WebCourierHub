using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Configuration
{
    public class NewsletterPOCOConfiguration : IEntityTypeConfiguration<NewsletterPOCO>
    {
        public void Configure(EntityTypeBuilder<NewsletterPOCO> builder)
        {
            // Define the table's primary key
            builder.HasKey(newsletter => newsletter.Id);

            // Define auto-increment for the primary key
            builder.Property(newsletter => newsletter.Id).ValueGeneratedOnAdd();

            builder.Property(newsletter => newsletter.Mail)
                   .IsRequired()
                   .HasMaxLength(255); // Assuming a max length of 255 characters for an email
        }
    }
}