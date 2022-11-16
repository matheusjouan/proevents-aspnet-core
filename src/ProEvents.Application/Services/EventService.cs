using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProEvents.Application.DTOs.Events;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Core.Models;

namespace ProEvents.Application.Services;
public class EventService : ObjectResult, IEventService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUploadFileService _uploadFileService;
    private const string _imgPath = "resources/images";

    public EventService(IUnitOfWork uow, IMapper mapper, 
        IUploadFileService uploadFileService)
    {
        _uow = uow;
        _mapper = mapper;
        _uploadFileService = uploadFileService;
    }

    public async Task<CustomResult> GetEventsAsync(long userId, PageParams pageParams, bool includeSpeaker = false)
    {
        var events = await _uow.EventRepository.GetAllEventsAsync(userId, pageParams ,includeSpeaker);

        var result = _mapper.Map<PageList<EventDto>>(events);

        result.CurrentPage = events.CurrentPage;
        result.TotalCount = events.TotalCount;
        result.PageSize = events.PageSize;
        result.TotalPages = events.TotalPages;

        AddResult(result);

        return _customResult;
    }

    public async Task<CustomResult> GetEventByIdAsync(long userId, long eventId, bool includeSpeaker = false)
    {
        var result = await GetResultEventByIdAsync(userId, eventId);
        Event evt = (Event)result.Result;

        AddResult(_mapper.Map<EventDto>(evt));
        return _customResult;
    }

    public async Task<CustomResult> AddEventAsync(long userId, CreateEventDto eventDto)
    {
        var evt = _mapper.Map<Event>(eventDto);
        evt.UserId = userId;

        _uow.EventRepository.Add(evt);
        await _uow.SaveChangesAsync();

        AddResult(_mapper.Map<EventDto>(evt));

        return _customResult;
    }

    public async Task<CustomResult> UpdateEventAsync(long userId, long eventId, UpdateEventDto eventDto)
    {
        var result = await GetResultEventByIdAsync(userId, eventId);
        Event evt = (Event)result.Result;

        eventDto.Id = eventId;
        eventDto.UserId = userId;

        _mapper.Map(eventDto, evt);

        _uow.EventRepository.Update(evt);
        await _uow.SaveChangesAsync();

        return _customResult;
    }

    public async Task<CustomResult> RemoveEventAsync(long userId, long eventId)
    {
        var result = await GetResultEventByIdAsync(userId, eventId);
        Event evt = (Event)result.Result;

        _uow.EventRepository.Delete(evt);
        _uploadFileService.DeleteImage(evt.ImageURL, _imgPath);

        await _uow.SaveChangesAsync();

        return _customResult;
    }

    public async Task<CustomResult> UploadImageAsync(long userId, long eventId, IFormFile file)
    {
        var result = await GetResultEventByIdAsync(userId, eventId);
        Event evt = (Event)result.Result;

        if (file.Length > 0)
        {
            _uploadFileService.DeleteImage(evt.ImageURL, _imgPath);
            evt.ImageURL = await _uploadFileService.SaveImage(file, _imgPath);
        }

        _uow.EventRepository.Update(evt);
        await _uow.SaveChangesAsync();

        return _customResult;
    }

    private async Task<CustomResult> GetResultEventByIdAsync(long userId, long eventId)
    {
        var evt = await _uow.EventRepository.GetEventByIdAsync(userId, eventId);

        if (evt == null)
        {
            AddError("Event not exist");
            return _customResult;
        }

        AddResult(evt);
        return _customResult;
    }
}
