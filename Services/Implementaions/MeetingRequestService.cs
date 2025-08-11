using System.Collections.Generic;
using System.Threading.Tasks;
using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

public class MeetingRequestService : IMeetingRequestService
{
    private readonly IMeetingRequestRepository _repository;

    public MeetingRequestService(IMeetingRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MeetingRequestDTO>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var dtos = new List<MeetingRequestDTO>();
        foreach (var e in entities)
        {
            dtos.Add(MapToDTO(e));
        }
        return dtos;
    }

    public async Task<MeetingRequestDTO?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        return MapToDTO(entity);
    }

    public async Task<MeetingRequestDTO> AddAsync(MeetingRequestDTO dto)
    {
        var entity = MapToEntity(dto);
        var added = await _repository.AddAsync(entity);
        return MapToDTO(added);
    }

    public async Task<MeetingRequestDTO> UpdateAsync(MeetingRequestDTO dto)
    {
        var entity = MapToEntity(dto);
        var updated = await _repository.UpdateAsync(entity);
        return MapToDTO(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    // Mapper Helpers
    private MeetingRequestDTO MapToDTO(MeetingRequest entity) => new MeetingRequestDTO
    {
        MeetingId = entity.MeetingId,
        Name = entity.Name,
        Email = entity.Email,
        Topic = entity.Topic,
        PreferredDate = entity.PreferredDate,
        MeetingStatus = entity.MeetingStatus,
        ResponseDate = entity.ResponseDate
    };

    private MeetingRequest MapToEntity(MeetingRequestDTO dto) => new MeetingRequest
    {
        MeetingId = dto.MeetingId,
        Name = dto.Name,
        Email = dto.Email,
        Topic = dto.Topic,
        PreferredDate = dto.PreferredDate,
        MeetingStatus = dto.MeetingStatus,
        ResponseDate = dto.ResponseDate
    };
}
