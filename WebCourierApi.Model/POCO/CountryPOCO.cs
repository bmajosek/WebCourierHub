using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.POCO
{
    public class CountryPOCO
    {
        public Country Id { get; set; }
        public string? Name { get; set; }
        public Currency CurrencyId { get; set; }
        public virtual CurrencyPOCO? Currency { get; set; }
        public virtual ICollection<InquirePOCO>? PickupInquires { get; set; }
        public virtual ICollection<InquirePOCO>? DeliveryInquires { get; set; }
    }
}
