using System.ComponentModel.DataAnnotations;

namespace Gutenburg_Server.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
