using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.POCO
{
    public class InquirePOCO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Package Package { get; set; }
        public DateOnly PickupDate { get; set; }
        public Country PickupCountryId { get; set; }
        public string? PickupZipCode { get; set; }
        public string? PickupTown { get; set; }
        public string? PickupStreet { get; set; }
        public int PickupBuildingNumber { get; set; }
        public int? PickupApartmentNumber { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public Country DeliveryCountryId { get; set; }
        public string? DeliveryZipCode { get; set; }
        public string? DeliveryTown { get; set; }
        public string? DeliveryStreet { get; set; }
        public int DeliveryBuildingNumber { get; set; }
        public int? DeliveryApartmentNumber { get; set; }
        public DeliveryOptions DeliveryOptions { get; set; }
        public string? OwnerKey { get; set; }
        public ICollection<OfferPOCO> Offers { get; set; } = null!;
        public virtual DeliveryPOCO? DeliveryRequest { get; set; }
        public virtual CountryPOCO? PickupCountry { get; set; }
        public virtual CountryPOCO? DeliveryCountry { get; set; }
    }
}
