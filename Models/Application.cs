using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gutenburg_Server.Models
{
    public enum ApplicationStatus
    {
        Accepted,
        Rejected,
        Pending
    }

    public class Application
    {
        [Key]
        public int ApplicationId { get; set;}
        [ForeignKey("Job")]
        public int JobId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string? Attachment { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }

        public Job Job { get; set; }
        public User User { get; set; }
    }

}
