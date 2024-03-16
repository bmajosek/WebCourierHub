﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCourierApi.Data;

#nullable disable

namespace WebCourierApi.Migrations
{
    [DbContext(typeof(WebCourierApiDbContext))]
    [Migration("20240122113920_addNewsletter")]
    partial class addNewsletter
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("WebCourierApi.Model.POCO.CountryPOCO", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            CurrencyId = 0,
                            Name = "Albania"
                        },
                        new
                        {
                            Id = 1,
                            CurrencyId = 9,
                            Name = "Andorra"
                        },
                        new
                        {
                            Id = 2,
                            CurrencyId = 1,
                            Name = "Armenia"
                        },
                        new
                        {
                            Id = 3,
                            CurrencyId = 9,
                            Name = "Austria"
                        },
                        new
                        {
                            Id = 4,
                            CurrencyId = 2,
                            Name = "Azerbaijan"
                        },
                        new
                        {
                            Id = 5,
                            CurrencyId = 5,
                            Name = "Belarus"
                        },
                        new
                        {
                            Id = 6,
                            CurrencyId = 9,
                            Name = "Belgium"
                        },
                        new
                        {
                            Id = 7,
                            CurrencyId = 3,
                            Name = "Bosnia and Herzegovina"
                        },
                        new
                        {
                            Id = 8,
                            CurrencyId = 4,
                            Name = "Bulgaria"
                        },
                        new
                        {
                            Id = 9,
                            CurrencyId = 9,
                            Name = "Croatia"
                        },
                        new
                        {
                            Id = 10,
                            CurrencyId = 9,
                            Name = "Cyprus"
                        },
                        new
                        {
                            Id = 11,
                            CurrencyId = 7,
                            Name = "Czech Republic"
                        },
                        new
                        {
                            Id = 12,
                            CurrencyId = 8,
                            Name = "Denmark"
                        },
                        new
                        {
                            Id = 13,
                            CurrencyId = 9,
                            Name = "Estonia"
                        },
                        new
                        {
                            Id = 14,
                            CurrencyId = 9,
                            Name = "Finland"
                        },
                        new
                        {
                            Id = 15,
                            CurrencyId = 9,
                            Name = "France"
                        },
                        new
                        {
                            Id = 16,
                            CurrencyId = 11,
                            Name = "Georgia"
                        },
                        new
                        {
                            Id = 17,
                            CurrencyId = 9,
                            Name = "Germany"
                        },
                        new
                        {
                            Id = 18,
                            CurrencyId = 9,
                            Name = "Greece"
                        },
                        new
                        {
                            Id = 19,
                            CurrencyId = 12,
                            Name = "Hungary"
                        },
                        new
                        {
                            Id = 20,
                            CurrencyId = 13,
                            Name = "Iceland"
                        },
                        new
                        {
                            Id = 21,
                            CurrencyId = 9,
                            Name = "Ireland"
                        },
                        new
                        {
                            Id = 22,
                            CurrencyId = 9,
                            Name = "Italy"
                        },
                        new
                        {
                            Id = 23,
                            CurrencyId = 9,
                            Name = "Latvia"
                        },
                        new
                        {
                            Id = 24,
                            CurrencyId = 6,
                            Name = "Liechtenstein"
                        },
                        new
                        {
                            Id = 25,
                            CurrencyId = 9,
                            Name = "Lithuania"
                        },
                        new
                        {
                            Id = 26,
                            CurrencyId = 9,
                            Name = "Luxembourg"
                        },
                        new
                        {
                            Id = 27,
                            CurrencyId = 9,
                            Name = "Malta"
                        },
                        new
                        {
                            Id = 28,
                            CurrencyId = 14,
                            Name = "Moldova"
                        },
                        new
                        {
                            Id = 29,
                            CurrencyId = 9,
                            Name = "Monaco"
                        },
                        new
                        {
                            Id = 30,
                            CurrencyId = 9,
                            Name = "Montenegro"
                        },
                        new
                        {
                            Id = 31,
                            CurrencyId = 9,
                            Name = "Netherlands"
                        },
                        new
                        {
                            Id = 32,
                            CurrencyId = 15,
                            Name = "North Macedonia"
                        },
                        new
                        {
                            Id = 33,
                            CurrencyId = 16,
                            Name = "Norway"
                        },
                        new
                        {
                            Id = 34,
                            CurrencyId = 17,
                            Name = "Poland"
                        },
                        new
                        {
                            Id = 35,
                            CurrencyId = 9,
                            Name = "Portugal"
                        },
                        new
                        {
                            Id = 36,
                            CurrencyId = 18,
                            Name = "Romania"
                        },
                        new
                        {
                            Id = 37,
                            CurrencyId = 20,
                            Name = "Russia"
                        },
                        new
                        {
                            Id = 38,
                            CurrencyId = 9,
                            Name = "San Marino"
                        },
                        new
                        {
                            Id = 39,
                            CurrencyId = 19,
                            Name = "Serbia"
                        },
                        new
                        {
                            Id = 40,
                            CurrencyId = 9,
                            Name = "Slovakia"
                        },
                        new
                        {
                            Id = 41,
                            CurrencyId = 9,
                            Name = "Slovenia"
                        },
                        new
                        {
                            Id = 42,
                            CurrencyId = 9,
                            Name = "Spain"
                        },
                        new
                        {
                            Id = 43,
                            CurrencyId = 21,
                            Name = "Sweden"
                        },
                        new
                        {
                            Id = 44,
                            CurrencyId = 6,
                            Name = "Switzerland"
                        },
                        new
                        {
                            Id = 45,
                            CurrencyId = 22,
                            Name = "Turkey"
                        },
                        new
                        {
                            Id = 46,
                            CurrencyId = 23,
                            Name = "Ukraine"
                        },
                        new
                        {
                            Id = 47,
                            CurrencyId = 10,
                            Name = "United Kingdom"
                        },
                        new
                        {
                            Id = 48,
                            CurrencyId = 9,
                            Name = "Vatican City"
                        },
                        new
                        {
                            Id = 49,
                            CurrencyId = 24,
                            Name = "United States"
                        });
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.CurrencyPOCO", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            ShortName = "ALL"
                        },
                        new
                        {
                            Id = 1,
                            ShortName = "AMD"
                        },
                        new
                        {
                            Id = 2,
                            ShortName = "AZN"
                        },
                        new
                        {
                            Id = 3,
                            ShortName = "BAM"
                        },
                        new
                        {
                            Id = 4,
                            ShortName = "BGN"
                        },
                        new
                        {
                            Id = 5,
                            ShortName = "BYN"
                        },
                        new
                        {
                            Id = 6,
                            ShortName = "CHF"
                        },
                        new
                        {
                            Id = 7,
                            ShortName = "CZK"
                        },
                        new
                        {
                            Id = 8,
                            ShortName = "DKK"
                        },
                        new
                        {
                            Id = 9,
                            ShortName = "EUR"
                        },
                        new
                        {
                            Id = 10,
                            ShortName = "GBP"
                        },
                        new
                        {
                            Id = 11,
                            ShortName = "GEL"
                        },
                        new
                        {
                            Id = 12,
                            ShortName = "HUF"
                        },
                        new
                        {
                            Id = 13,
                            ShortName = "ISK"
                        },
                        new
                        {
                            Id = 14,
                            ShortName = "MDL"
                        },
                        new
                        {
                            Id = 15,
                            ShortName = "MKD"
                        },
                        new
                        {
                            Id = 16,
                            ShortName = "NOK"
                        },
                        new
                        {
                            Id = 17,
                            ShortName = "PLN"
                        },
                        new
                        {
                            Id = 18,
                            ShortName = "RON"
                        },
                        new
                        {
                            Id = 19,
                            ShortName = "RSD"
                        },
                        new
                        {
                            Id = 20,
                            ShortName = "RUB"
                        },
                        new
                        {
                            Id = 21,
                            ShortName = "SEK"
                        },
                        new
                        {
                            Id = 22,
                            ShortName = "TRY"
                        },
                        new
                        {
                            Id = 23,
                            ShortName = "UAH"
                        },
                        new
                        {
                            Id = 24,
                            ShortName = "USD"
                        });
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.DeliveryPOCO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("InquireId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPending")
                        .HasColumnType("BIT");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("DATETIME");

                    b.Property<decimal>("PricingBase")
                        .HasColumnType("MONEY");

                    b.Property<int>("PricingCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PricingFees")
                        .HasColumnType("MONEY");

                    b.Property<decimal>("PricingTaxes")
                        .HasColumnType("MONEY");

                    b.ComplexProperty<Dictionary<string, object>>("Client", "WebCourierApi.Model.POCO.DeliveryPOCO.Client#Client", b1 =>
                        {
                            b1.Property<string>("CompanyName")
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<string>("EmailAddress")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<string>("FirstName")
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<string>("LastName")
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("InquireId")
                        .IsUnique();

                    b.HasIndex("PricingCurrencyId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.InquirePOCO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<int?>("DeliveryApartmentNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeliveryBuildingNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeliveryCountryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DeliveryDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("DeliveryStreet")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeliveryTown")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeliveryZipCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("PickupApartmentNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PickupBuildingNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PickupCountryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("PickupDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("PickupStreet")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PickupTown")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PickupZipCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("DeliveryOptions", "WebCourierApi.Model.POCO.InquirePOCO.DeliveryOptions#DeliveryOptions", b1 =>
                        {
                            b1.Property<bool>("HighPriority")
                                .HasColumnType("BIT");

                            b1.Property<bool>("IsForCompany")
                                .HasColumnType("BIT");

                            b1.Property<bool>("WeekendDelivery")
                                .HasColumnType("BIT");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Package", "WebCourierApi.Model.POCO.InquirePOCO.Package#Package", b1 =>
                        {
                            b1.Property<float>("HeightCM")
                                .HasColumnType("REAL");

                            b1.Property<float>("LengthCM")
                                .HasColumnType("REAL");

                            b1.Property<float>("WeightKG")
                                .HasColumnType("REAL");

                            b1.Property<float>("WidthCM")
                                .HasColumnType("REAL");
                        });

                    b.HasKey("Id");

                    b.HasIndex("DeliveryCountryId");

                    b.HasIndex("PickupCountryId");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.NewsletterPOCO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.CountryPOCO", b =>
                {
                    b.HasOne("WebCourierApi.Model.POCO.CurrencyPOCO", "Currency")
                        .WithMany("Countries")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.DeliveryPOCO", b =>
                {
                    b.HasOne("WebCourierApi.Model.POCO.InquirePOCO", "Inquire")
                        .WithOne("DeliveryRequest")
                        .HasForeignKey("WebCourierApi.Model.POCO.DeliveryPOCO", "InquireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCourierApi.Model.POCO.CurrencyPOCO", "PricingCurrency")
                        .WithMany("Deliveries")
                        .HasForeignKey("PricingCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WebCourierApi.Model.POCO.DeliveryProcessPOCO", "Process", b1 =>
                        {
                            b1.Property<int>("DeliveryRequestId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("DeliveryCourierName")
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<DateTime?>("DeliveryTimestamp")
                                .HasColumnType("DATETIME");

                            b1.Property<bool>("IsDelivered")
                                .HasColumnType("BIT");

                            b1.Property<string>("Notes")
                                .HasMaxLength(500)
                                .HasColumnType("TEXT");

                            b1.Property<string>("PickupCourierName")
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");

                            b1.Property<DateTime?>("PickupTimestamp")
                                .HasColumnType("DATETIME");

                            b1.HasKey("DeliveryRequestId");

                            b1.ToTable("Processes", (string)null);

                            b1.WithOwner("DeliveryRequest")
                                .HasForeignKey("DeliveryRequestId");

                            b1.Navigation("DeliveryRequest");
                        });

                    b.Navigation("Inquire");

                    b.Navigation("PricingCurrency");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.InquirePOCO", b =>
                {
                    b.HasOne("WebCourierApi.Model.POCO.CountryPOCO", "DeliveryCountry")
                        .WithMany("DeliveryInquires")
                        .HasForeignKey("DeliveryCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebCourierApi.Model.POCO.CountryPOCO", "PickupCountry")
                        .WithMany("PickupInquires")
                        .HasForeignKey("PickupCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("WebCourierApi.Model.POCO.OfferPOCO", "Offers", b1 =>
                        {
                            b1.Property<int>("OfferNumber")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("InquireId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal>("PricingBase")
                                .HasColumnType("MONEY");

                            b1.Property<int>("PricingCurrencyId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal>("PricingFees")
                                .HasColumnType("MONEY");

                            b1.Property<decimal>("PricingTaxes")
                                .HasColumnType("MONEY");

                            b1.Property<DateTime>("ValidTo")
                                .HasColumnType("DATETIME");

                            b1.HasKey("OfferNumber", "InquireId");

                            b1.HasIndex("InquireId");

                            b1.HasIndex("PricingCurrencyId");

                            b1.ToTable("Offers", (string)null);

                            b1.WithOwner("Inquire")
                                .HasForeignKey("InquireId");

                            b1.HasOne("WebCourierApi.Model.POCO.CurrencyPOCO", "PricingCurrency")
                                .WithMany()
                                .HasForeignKey("PricingCurrencyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Inquire");

                            b1.Navigation("PricingCurrency");
                        });

                    b.Navigation("DeliveryCountry");

                    b.Navigation("Offers");

                    b.Navigation("PickupCountry");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.CountryPOCO", b =>
                {
                    b.Navigation("DeliveryInquires");

                    b.Navigation("PickupInquires");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.CurrencyPOCO", b =>
                {
                    b.Navigation("Countries");

                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("WebCourierApi.Model.POCO.InquirePOCO", b =>
                {
                    b.Navigation("DeliveryRequest");
                });
#pragma warning restore 612, 618
        }
    }
}
