namespace WebCourierHub.Classes
{
    public class OfferAdapterDTO2
    {
        public string InquiryId { get; set; }
        public float TotalPrice { get; set; }
        public string Currency { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakdownAdapterDTO2> PriceBreakDown { get; set; }
    }

    public class PriceBreakdownAdapterDTO2
    {
        public float Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}