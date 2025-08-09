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

        [Required]
        public string Name { get; set; }    

        [Required]
        [EmailAddress]
        public string Email { get; set; }  
        [Required]
        public string Topic { get; set; }

        public DateTime PreferredDate { get; set; }

        public MeetingStatus MeetingStatus { get; set; }

        public DateTime? ResponseDate { get; set; }
    }

}