using WebCourierHub.Models;

namespace WebCourierHub.Classes
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public string NIP { get; set; }
    }
}