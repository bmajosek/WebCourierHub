using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.POCO
{
    public class CurrencyPOCO
    {
        public Currency Id { get; set; }
        public string? ShortName { get; set; }
        public virtual ICollection<CountryPOCO>? Countries { get; set; }
        public virtual ICollection<DeliveryPOCO>? Deliveries { get; set; }
    }
}
