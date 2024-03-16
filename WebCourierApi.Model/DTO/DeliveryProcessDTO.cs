namespace WebCourierApi.Model.DTO
{
    public class DeliveryProcessDTO
    {
        public string? DeliveryStatus {  get; set; }
        public string? PickupCourierName { get; set; }
        public DateTime? PickupTimestamp { get; set; }
        public string? DeliveryCourierName { get; set; }
        public DateTime? DeliveryTimestamp { get; set; }
        public string? Notes { get; set; }
    }
}
