namespace WebCourierHub.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public Status? Status { get; set; }
        public Company? Company { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public int? ExternalId { get; set; }
        public string CourierName { get; set; }
        public int? CourierId { get; set; }
    }
}