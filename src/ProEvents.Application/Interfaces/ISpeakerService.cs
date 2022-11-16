using ProEvents.Application.DTOs.Speakers;
using ProEvents.Core.Models;

namespace ProEvents.Application.Interfaces;
public interface ISpeakerService
{
    Task<CustomResult> AddSpeakerAsync(long userId, CreateSpeakerDto model);
    Task<CustomResult> UpdateSpeakerAsync(long userId, UpdateSpeakerDto model);
    Task<CustomResult> GetAllSpeakersAsync(PageParams pageParams, bool includeEvents = false);
    Task<CustomResult> GetSpeakerByUserIdAsync(long userId, bool includeEvents = false);
}
