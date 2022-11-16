using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Extensions;
using ProEvents.Application.DTOs.Events;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Models;

namespace ProEvents.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class EventController : MainController
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEventsAsync([FromQuery] PageParams pageParams)
    {
        var userId = User.GetUserId();
        var result = await _eventService.GetEventsAsync(userId, pageParams);

        PageList<EventDto> pagedEvts = (PageList<EventDto>)result.Result;

        Response.AddPaginationHeader(pagedEvts.CurrentPage, pagedEvts.TotalPages,
            pagedEvts.PageSize, pagedEvts.TotalCount);

        return CustomResponse(result);
    }


    [HttpGet("{id:long}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventByIdAsync(long id)
    {
        var userId = User.GetUserId();
        
        var result = await _eventService.GetEventByIdAsync(userId, id);

        if (!result.IsValid)
            return NotFound(result);

        return CustomResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventDto request)
    {
        var userId = User.GetUserId();
        var result = await _eventService.AddEventAsync(userId, request);

        EventDto evt = (EventDto)result.Result;

        return new CreatedAtRouteResult("GetEventById", new { id = evt.Id }, evt);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateEventAsync(long id, UpdateEventDto request)
    {
        var userId = User.GetUserId();

        var result = await _eventService.UpdateEventAsync(userId, id, request);

        if (!result.IsValid)
            return NotFound(result);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteEventAsync(long id)
    {
        var userId = User.GetUserId();
        var result = await _eventService.RemoveEventAsync(userId, id);

        if (!result.IsValid)
            return NotFound(result);

        return NoContent();
    }

    [HttpPost("upload-image/{id:long}")]
    public async Task<IActionResult> UploadFileAsync(long id)
    {
        var userId = User.GetUserId();
        var file = Request.Form.Files[0];
        
        var result = await _eventService.UploadImageAsync(userId, id, file);

        if (!result.IsValid)
            return NotFound(result); 

        return NoContent();
    }
}
