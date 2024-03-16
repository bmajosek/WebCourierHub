using WebCourierApi.Model.ValueObjects;

namespace WebCourierApi.Model.DTO
{
    public class InquireDetailsDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Package Package { get; set; }
        public DateOnly PickupDate { get; set; }
        public AddressDTO PickupAddress { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public AddressDTO DeliveryAddress { get; set; }
        public DeliveryOptions DeliveryOptions { get; set; }
    }
}
