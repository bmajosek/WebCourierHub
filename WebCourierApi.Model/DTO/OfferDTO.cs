
namespace WebCourierApi.Model.DTO
{
    public class OfferDTO
    {
        public int OfferNumber { get; set; }
        public DateTime ValidTo { get; set; }
        public PricingDTO Pricing { get; set; }
    }
}
