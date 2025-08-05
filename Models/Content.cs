using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gutenburg_Server.Models
{
    public enum PageType
    {
        Main,
        ContactUs,
        Services,
        Solutions,
        Jobs
    }

    public class Content
    {
        [Key]
        public int ContentId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
         [Required]
        public string Body { get; set; }
        [Required]
        public PageType PageType { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public User User { get; set; }
    }

}
