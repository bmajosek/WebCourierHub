using WebCourierHub.Models;

namespace WebCourierHub.Classes
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string CompanyName { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public string CourierName { get; set; }
    }
}