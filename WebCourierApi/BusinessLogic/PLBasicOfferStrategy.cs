using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.BusinessLogic
{
    public class PLBasicOfferStrategy : IOfferStrategy
    {
        public Pricing EvalPricing(Inquire inquire) => new Pricing()
        {
            Base = 10.0m * (decimal)inquire.Package.WeightKG,
            Taxes = 0,
            Fees = 10m,
            CurrencyId = Currency.PLN, 
            Currency = string.Empty
        };

        public DateTime EvalValidTo(Inquire inquire) => DateTime.Today.AddDays(1);

        public bool IsApplicable(Inquire inquire)
        {
            return inquire.PickupAddress.CountryId == Country.Poland
                && inquire.DeliveryAddress.CountryId == Country.Poland;
        }
    }
}
