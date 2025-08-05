using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gutenburg_Server.Models
{
    public enum MeetingStatus
    {
        Accepted,
        Pending,
        Postponed
    }

    public class MeetingRequest
    {
        [Key]
        public int MeetingId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Topic { get; set; }
        public DateTime PreferredDate { get; set; }
      
        public MeetingStatus MeetingStatus { get; set; }
        public DateTime? ResponseDate { get; set; }

        public User User { get; set; }
    }

}
