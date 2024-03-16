using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.BusinessLogic
{
    public class EUBasicOfferStrategy : IOfferStrategy
    {
        public Pricing EvalPricing(Inquire inquire) => new Pricing()
        {
            Base = 3.0m * (decimal)inquire.Package.WeightKG,
            Taxes = 5m,
            Fees = 3m,
            CurrencyId = Currency.EUR, 
            Currency = string.Empty
        };

        public DateTime EvalValidTo(Inquire inquire) => DateTime.Today.AddDays(7);

        public bool IsApplicable(Inquire inquire)
        {
            return inquire.PickupAddress.CountryId != Country.Russia
                && inquire.DeliveryAddress.CountryId != Country.Russia;
        }
    }
}
