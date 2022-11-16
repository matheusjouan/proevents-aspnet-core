using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Extensions;
using ProEvents.Application.DTOs.Events;
using ProEvents.Application.DTOs.SocialNetworks;
using ProEvents.Application.DTOs.Speakers;
using ProEvents.Application.Interfaces;

namespace ProEvents.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class SocialNetworkController : MainController
{
    private readonly ISocialNetworkService _socialnetworkService;
    private readonly IEventService _eventService;
    private readonly ISpeakerService _speakerService;

    public SocialNetworkController(ISocialNetworkService socialnetworkService, 
        IEventService eventService, ISpeakerService speakerService)
    {
        _socialnetworkService = socialnetworkService;
        _eventService = eventService;
        _speakerService = speakerService;
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetAllSocialNetworksByEventAsync(long eventId)
    {
        var userId = User.GetUserId();
        
        // Verifica se é Autor do evento
        var evtResult = await _eventService.GetEventByIdAsync(userId, eventId);
        
        if (!evtResult.IsValid)
            return Unauthorized();

        var results = await _socialnetworkService.GetAllSocialNetworkByEventAsync(eventId);

        return CustomResponse(results);
    }

    [HttpGet("speaker")]
    public async Task<IActionResult> GetAllSocialNetworksBySpeakerAsync()
    {
        var userId = User.GetUserId();

        var resultSpeaker = await _speakerService.GetSpeakerByUserIdAsync(userId);

        if (!resultSpeaker.IsValid)
            return Unauthorized();

        SpeakerDto speaker = (SpeakerDto)resultSpeaker.Result; 

        var results = await _socialnetworkService.GetAllSocialNetworkBySpeakerAsync(speaker.Id);

        return CustomResponse(results);
    }


    [HttpPut("event/{eventId}")]
    public async Task<IActionResult> SaveSocialNetworksByEventAsync(long eventId, IEnumerable<SaveSocialNetworkDto> request)
    {
        var userId = User.GetUserId();

        // Verifica se é Autor do evento
        var evtResult = await _eventService.GetEventByIdAsync(userId, eventId);

        if (!evtResult.IsValid)
            return Unauthorized();

        await _socialnetworkService.SaveSocialNetworkByEventAsync(eventId, request);

        return NoContent();
    }

    [HttpPut("speaker")]
    public async Task<IActionResult> SaveSocialNetworksBySpeakerAsync(IEnumerable<SaveSocialNetworkDto> request)
    {
        var userId = User.GetUserId();
        
        var resultSpeaker = await _speakerService.GetSpeakerByUserIdAsync(userId);

        if (!resultSpeaker.IsValid)
            return Unauthorized();

        SpeakerDto speaker = (SpeakerDto)resultSpeaker.Result;

        await _socialnetworkService.SaveSocialNetworkBySpeakerAsync(speaker.Id, request);

        return NoContent();
    }

    [HttpDelete("{id}/event/{eventId}")]
    public async Task<IActionResult> DeleteSocialNetworkByEventAsync(long eventId, long id)
    {
        var userId = User.GetUserId();

        // Verificar se o usuário é dono do evento
        var resultEvt = await _eventService.GetEventByIdAsync(userId, eventId);

        if (!resultEvt.IsValid)
            return Unauthorized();

        var result = await _socialnetworkService.DeleteSocialNetworkByEventAsync(id, eventId);
        if (!result.IsValid)
            return NotFound(result.Errors);

        return NoContent();
    }

    [HttpDelete("{id}/speaker")]
    public async Task<IActionResult> DeleteSocialNetworkBySpeakerAsync(long id)
    {
        var userId = User.GetUserId();

        var speakerResult = await _speakerService.GetSpeakerByUserIdAsync(userId);

        if (!speakerResult.IsValid)
            return Unauthorized();

        var speaker = (SpeakerDto)speakerResult.Result;

        await _socialnetworkService.DeleteSocialNetworkBySpeakerAsync(id, speaker.Id);
        return NoContent();
    }
}
