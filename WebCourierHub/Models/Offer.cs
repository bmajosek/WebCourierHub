namespace WebCourierHub.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public Status? Status { get; set; }
        public Company? Company { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ExternalId { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? ClientId { get; set; }
    }
}