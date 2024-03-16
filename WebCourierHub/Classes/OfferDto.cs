using WebCourierHub.Models;

namespace WebCourierHub.Classes
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public decimal? TotalPrice { get; set; }
        public string ClientName { get; set; }
    }
}