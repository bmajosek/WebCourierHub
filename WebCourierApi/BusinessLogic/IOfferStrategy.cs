using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;

namespace WebCourierApi.BusinessLogic
{
    public interface IOfferStrategy
    {
        bool IsApplicable(Inquire inquire);
        Pricing EvalPricing(Inquire inquire);
        DateTime EvalValidTo(Inquire inquire);
    }
}
