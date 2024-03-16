using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.BusinessLogic
{
    public class USABasicOfferStrategy : IOfferStrategy
    {
        public Pricing EvalPricing(Inquire inquire) => new Pricing()
        {
            Base = 5.0m * (decimal)inquire.Package.WeightKG,
            Taxes = 2m,
            Fees = 5m,
            CurrencyId = Currency.USD,
            Currency = string.Empty
        };

        public DateTime EvalValidTo(Inquire inquire) => DateTime.Today.AddDays(5);

        public bool IsApplicable(Inquire inquire)
        {
            return inquire.PickupAddress.CountryId == Country.UnitedStates
                && inquire.DeliveryAddress.CountryId == Country.UnitedStates;
        }
    }
}