using System.ComponentModel.DataAnnotations;

namespace Gutenburg_Server.Models
{
    public enum SolutionType
    {
        Custom,
        ReadyMade
    }

    public class Solution
    {
        [Key]
        public int SolutionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public SolutionType SolutionType { get; set; }
        public string Image { get; set; }
    }

}
