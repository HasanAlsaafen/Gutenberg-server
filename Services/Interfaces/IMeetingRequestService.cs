using System.Collections.Generic;
using System.Threading.Tasks;
using Gutenburg_Server.DTOs;

public interface IMeetingRequestService
{
    Task<IEnumerable<MeetingRequestDTO>> GetAllAsync();
    Task<MeetingRequestDTO?> GetByIdAsync(int id);
    Task<MeetingRequestDTO> AddAsync(MeetingRequestDTO dto);
    Task<MeetingRequestDTO> UpdateAsync(MeetingRequestDTO dto);
    Task<bool> DeleteAsync(int id);
}
