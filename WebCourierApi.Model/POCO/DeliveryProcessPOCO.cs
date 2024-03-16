
namespace WebCourierApi.Model.POCO
{
    public class DeliveryProcessPOCO
    {
        public bool IsDelivered { get; set; }
        public string? PickupCourierName { get; set; }
        public string? DeliveryCourierName { get; set; }
        public DateTime? PickupTimestamp { get; set; }
        public DateTime? DeliveryTimestamp { get; set; }
        public string? Notes { get; set; }
        public int DeliveryRequestId { get; set; }
        public DeliveryPOCO? DeliveryRequest { get; set; }
    }
}
