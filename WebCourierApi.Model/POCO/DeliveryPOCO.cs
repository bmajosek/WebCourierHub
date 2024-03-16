using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.POCO
{
    public class DeliveryPOCO
    {
        public int Id { get; set; }
        public int InquireId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public decimal PricingBase { get; set; }
        public decimal PricingTaxes { get; set; }
        public decimal PricingFees { get; set; }
        public Currency PricingCurrencyId { get; set; }
        public Client Client { get; set; }
        public bool IsPending { get; set; }
        public virtual DeliveryProcessPOCO? Process { get; set; }
        public virtual InquirePOCO? Inquire { get; set; }
        public virtual CurrencyPOCO? PricingCurrency { get; set; }
    }
}
