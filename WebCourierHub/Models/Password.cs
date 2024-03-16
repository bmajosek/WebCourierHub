using Microsoft.Build.Framework;

namespace WebCourierHub.Models
{
    public class Password
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}