namespace WebCourierHub.Classes
{
    public class UserInformation
    {
        public string OfferUuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public AddressDetails Address { get; set; }

        public class AddressDetails
        {
            public string BuildingNumber { get; set; }
            public string ApartmentNumber { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
        }
    }
}