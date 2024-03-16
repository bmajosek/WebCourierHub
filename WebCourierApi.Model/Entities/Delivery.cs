using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.Entities
{
    public record Delivery
    {
        public int Id { get; set; }
        public required Inquire Inquire { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Pricing Pricing { get; set; }
        public Client Client { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public DeliveryProcess? Process { get; set; }
    }
}
