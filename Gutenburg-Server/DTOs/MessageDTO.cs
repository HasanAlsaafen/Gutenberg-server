namespace Gutenburg_Server.DTOs
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public DateTime DateSent { get; set; }

    }
}
