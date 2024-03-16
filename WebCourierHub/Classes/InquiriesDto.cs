using WebCourierHub.Models;

namespace WebCourierHub.Classes
{
    public class InquiriesDto
    {
        public List<InquiryDto>? Inquiries { get; set; }
        public string IdentityName { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
    }
}