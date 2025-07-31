using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gutenburg_Server.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]

        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }

        public User User { get; set; }
        public ICollection<Application> Applications { get; set; }
    }

}
