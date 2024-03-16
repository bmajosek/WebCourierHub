namespace WebCourierHub.Classes
{
    public class NewInquiryDTO
    {
        public string InquiryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDTO4 Address { get; set; }
    }

    public class AddressDTO4
    {
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}