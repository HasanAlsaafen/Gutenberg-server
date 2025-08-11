using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;

namespace Gutenburg_Server.Models
{
    public enum Role { Admin, User }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } // Hashed
        [Required]

        public Role Role { get; set; }

        public ICollection<Job> Jobs { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<MeetingRequest> MeetingRequests { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Content> Contents { get; set; }

    }

}
