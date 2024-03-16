using WebCourierApi.Model.ValueObjects;

namespace WebCourierApi.Model.Entities
{
    public record Inquire
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Package Package { get; set; }
        public DateOnly PickupDate { get; set; }
        public Address PickupAddress { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public Address DeliveryAddress { get; set; }
        public DeliveryOptions DeliveryOptions { get; set; }
    }
}
