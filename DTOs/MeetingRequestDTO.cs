using Gutenburg_Server.Models;

namespace Gutenburg_Server.DTOs
{
public class MeetingRequestDTO
{
    public int MeetingId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public DateTime PreferredDate { get; set; }
    public MeetingStatus MeetingStatus { get; set; }
    public DateTime? ResponseDate { get; set; }
}

}
