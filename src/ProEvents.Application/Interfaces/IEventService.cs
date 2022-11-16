using Microsoft.AspNetCore.Http;
using ProEvents.Application.DTOs.Events;
using ProEvents.Core.Models;

namespace ProEvents.Application.Interfaces;
public interface IEventService
{
    Task<CustomResult> GetEventsAsync(long userId, PageParams pageParams, bool includeSpeaker = false);
    Task<CustomResult> GetEventByIdAsync(long userId, long eventId, bool includeSpeaker = false);
    Task<CustomResult> AddEventAsync(long userId, CreateEventDto eventDto);
    Task<CustomResult> UpdateEventAsync(long userId, long eventId, UpdateEventDto eventDto);
    Task<CustomResult> RemoveEventAsync(long userId, long eventId);
    Task<CustomResult> UploadImageAsync(long userId, long eventId, IFormFile file);
}
