namespace Gutenburg_Server.DTOs
{
    public class ApplicationDTO
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public string ApplicantEmail { get; set; } = string.Empty;
        public string ApplicantPhone { get; set; } = string.Empty;
        public string Attachment { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; } = "Pending";
    }
}
