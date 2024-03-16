namespace WebCourierHub.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string NIP { get; set; }
    }
}