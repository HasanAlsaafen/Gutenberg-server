namespace Gutenburg_Server.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }
        public string PostedBy { get; set; } 
        public int UserId { get; set; }


    }
}
