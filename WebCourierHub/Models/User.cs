namespace WebCourierHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityName { get; set; }
        public Company? Company { get; set; }
    }
}