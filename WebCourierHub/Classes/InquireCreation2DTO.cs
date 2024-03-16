using WebCourierApi.Model.DTO;
using WebCourierApi.Model.ValueObjects;

namespace WebCourierHub.Classes
{
    public class InquireCreation2DTO
    {
        public DimensionsDTO Dimensions { get; set; }
        public string Currency { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public AddressDTO2 Source { get; set; }
        public AddressDTO2 Destination { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDay { get; set; }
        public bool DeliveryInWeekend { get; set; }
        public string Priority { get; set; }
        public bool VipPackage { get; set; }
        public bool IsCompany { get; set; }
    }
}