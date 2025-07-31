using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gutenburg_Server.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string MessageContent { get; set; }
        public DateTime DateSent { get; set; }

        public User User { get; set; }
    }

}
