namespace WebCourierHub.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public Address Destination { get; set; }
        public Address Source { get; set; }
        public Package Package { get; set; }
        public bool? Priority { get; set; }
        public Company? Company { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool? DeliveryAtWeekend { get; set; }
        public Status? Status { get; set; }
    }
}