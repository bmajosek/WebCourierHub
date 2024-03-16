using Microsoft.Build.Framework;

namespace WebCourierHub.Models
{
    public class Role
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}