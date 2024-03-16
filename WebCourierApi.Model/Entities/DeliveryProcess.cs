using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.Entities
{
    public record DeliveryProcess
    {
        public int Id { get; init; }
        public bool IsDelivered { get; set; }
        public string? PickupCourierName { get; set; }
        public string? DeliveryCourierName { get; set; }
        public DateTime? PickupTimestamp { get; set; }
        public DateTime? DeliveryTimestamp { get; set; }
        public string? Notes { get; set; }
        public DeliveryStatus DeliveryStatus => PickupTimestamp == null 
            ? DeliveryStatus.Started 
            : DeliveryTimestamp == null 
                ? DeliveryStatus.PickedUp 
                : IsDelivered
                    ? DeliveryStatus.Delivered
                    : DeliveryStatus.CannotDeliver;
    }
}
