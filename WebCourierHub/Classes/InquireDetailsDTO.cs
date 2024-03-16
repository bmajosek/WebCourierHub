namespace WebCourierHub.Classes
{
    public class InquireDetailsDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public PackageAdapter Package { get; set; }
        public DateTime PickupDate { get; set; }
        public AddressAdapter PickupAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public AddressAdapter DeliveryAddress { get; set; }
        public DeliveryOptionsAdapter DeliveryOptions { get; set; }
    }
}