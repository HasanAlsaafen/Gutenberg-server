namespace Gutenburg_Server.DTOs
{

    public enum ApplicationStatusDTO
    {
        Accepted,
        Rejected,
        Pending
    }
    public class ApplicationDTO
    {
    
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string Attachment { get; set; }
        public DateTime ApplicationDate { get; set; }
        public ApplicationStatusDTO AppStatus { get; set; }

    }
}
