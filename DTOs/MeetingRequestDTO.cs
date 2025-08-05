namespace Gutenburg_Server.DTOs
{
    public class MeetingRequestDTO
    {
        public int MeetingId { get; set; }
        public int UserId { get; set; }
        public string Topic { get; set; }
        public DateTime PreferredDate { get; set; }
        public string MeetingStatus { get; set; }
        public DateTime? ResponseDate { get; set; }

    }
}
