using WebCourierApi.Model.ValueObjects;

namespace WebCourierApi.Model.DTO
{
    public class DeliveryDTO
    {
        public int Id { get; set; }
        public InquireDetailsDTO? Inquire { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public PricingDTO Pricing { get; set; }
        public Client Client { get; set; }
        public string? RequestStatus { get; set; }
        public DeliveryProcessDTO? Process { get; set; }
    }
}
