using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.POCO
{
    public class OfferPOCO
    {
        public int InquireId { get; set; }
        public int OfferNumber { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal PricingBase { get; set; }
        public decimal PricingTaxes { get; set; }
        public decimal PricingFees { get; set; }
        public Currency PricingCurrencyId { get; set; }
        public virtual InquirePOCO? Inquire { get; set; }
        public virtual CurrencyPOCO? PricingCurrency { get; set; }
    }
}
