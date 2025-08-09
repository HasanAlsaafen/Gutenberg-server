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
        public int ApplicationId { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Required]
        [StringLength(100)]
        public string ApplicantName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string ApplicantEmail { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string ApplicantPhone { get; set; } = string.Empty;

        [Required]
        public string Attachment { get; set; } = string.Empty;

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        public ApplicationStatus ApplicationStatus { get; set; } = ApplicationStatus.Pending;

        public Job Job { get; set; }
    }
}
