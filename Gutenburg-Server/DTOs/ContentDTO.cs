namespace Gutenburg_Server.DTOs
{
    public class ContentDTO
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string PageType { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
