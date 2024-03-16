using WebCourierApi.Model.ValueObjects;

namespace WebCourierApi.Model.DTO
{
    public class InquireCreationDTO
    {
        public Package Package { get; set; }
        public DateOnly PickupDate { get; set; }
        public AddressDTO PickupAddress { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public AddressDTO DeliveryAddress { get; set; }
        public DeliveryOptions DeliveryOptions { get; set; }
    }
}