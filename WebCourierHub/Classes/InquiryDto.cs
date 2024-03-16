using System;

namespace WebCourierHub.Classes
{
    public class InquiryDto
    {
        public int? Id { get; set; }
        public string ClientId { get; set; }
        public AddressDto Destination { get; set; }
        public AddressDto Source { get; set; }
        public PackageDto Package { get; set; }
        public CompanyDto Company { get; set; }
        public StatusDto Status { get; set; }

        public bool Priority { get; set; }

        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool DeliveryAtWeekend { get; set; }
    }
}