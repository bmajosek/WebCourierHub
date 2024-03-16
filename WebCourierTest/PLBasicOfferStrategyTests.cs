using WebCourierApi.BusinessLogic;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.Enums;
using WebCourierApi.Model.ValueObjects;

namespace WebCourierTest
{
    public class PLBasicOfferStrategyTests
    {
        [Fact]
        public void EvalPricing_ReturnsCorrectPricing_ForGivenWeightPL()
        {
            var strategy = new PLBasicOfferStrategy();
            var inquire = new Inquire
            {
                Package = new Package { WeightKG = 2, HeightCM = 3, LengthCM = 5, WidthCM = 5 }
            };

            var pricing = strategy.EvalPricing(inquire);

            Assert.Equal(20m, pricing.Base);
            Assert.Equal(0m, pricing.Taxes);
            Assert.Equal(10m, pricing.Fees);
        }

        [Fact]
        public void EvalValidTo_ReturnsDateOneDayAheadPL()
        {
            var strategy = new PLBasicOfferStrategy();
            var inquire = new Inquire();

            var validToDate = strategy.EvalValidTo(inquire);

            Assert.Equal(DateTime.Today.AddDays(1), validToDate);
        }

        [Fact]
        public void IsApplicable_ReturnsTrue_ForPolandAddresses()
        {
            var strategy = new PLBasicOfferStrategy();
            var inquire = new Inquire
            {
                Id = 123,
                CreationDate = DateTime.Now,
                Package = new Package
                {
                    WeightKG = 2,
                    HeightCM = 30,
                    LengthCM = 40,
                    WidthCM = 20
                },
                PickupDate = DateOnly.FromDateTime(DateTime.Today),
                PickupAddress = new Address
                {
                    CountryId = Country.Poland,
                    Country = "Poland",
                    ZipCode = "00-001",
                    Town = "Warsaw",
                    Street = "Main Street",
                    BuildingNumber = 10,
                    ApartmentNumber = 2
                },
                DeliveryAddress = new Address
                {
                    CountryId = Country.Poland,
                    Country = "Poland",
                    ZipCode = "00-002",
                    Town = "Krakow",
                    Street = "Second Street",
                    BuildingNumber = 20,
                    ApartmentNumber = null
                },
                DeliveryDate = DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                DeliveryOptions = new DeliveryOptions()
            };

            var isApplicable = strategy.IsApplicable(inquire);

            Assert.True(isApplicable);
        }
    }
}