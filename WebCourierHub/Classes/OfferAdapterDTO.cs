namespace WebCourierHub.Classes
{
    public class OfferAdapterDTO
    {
        public int OfferNumber { get; set; }
        public DateTime ValidTo { get; set; }
        public PricingAdapterDTO Pricing { get; set; }
    }

    public class PricingAdapterDTO
    {
        public decimal Base { get; init; }
        public decimal Taxes { get; init; }
        public decimal Fees { get; init; }
        public string Currency { get; init; }
        public decimal Total => Base + Taxes + Fees;
    }
}