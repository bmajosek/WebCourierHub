namespace WebCourierHub.Classes
{
    public class InquiresAdapterDTO
    {
        public PackageAdapter Package { get; set; }
        public DateOnly PickupDate { get; set; }
        public AddressAdapter PickupAddress { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public AddressAdapter DeliveryAddress { get; set; }
        public DeliveryOptionsAdapter DeliveryOptions { get; set; }
    }

    public class PackageAdapter
    {
        public float LengthCM { get; init; }
        public float WidthCM { get; init; }
        public float HeightCM { get; init; }
        public float WeightKG { get; init; }
    }

    public class AddressAdapter
    {
        public string Country { get; init; }
        public string ZipCode { get; init; }
        public string Town { get; init; }
        public string Street { get; init; }
        public int BuildingNumber { get; init; }
        public int? ApartmentNumber { get; init; }
    }

    public class DeliveryOptionsAdapter
    {
        public bool IsForCompany { get; init; }
        public bool HighPriority { get; init; }
        public bool WeekendDelivery { get; init; }
    }
}