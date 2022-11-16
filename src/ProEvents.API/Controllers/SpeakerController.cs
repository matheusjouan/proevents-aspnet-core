using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Extensions;
using ProEvents.Application.DTOs.Speakers;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Models;

namespace ProEvents.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class SpeakerController : MainController
{
	private readonly ISpeakerService _speakerService;
	
	public SpeakerController(ISpeakerService speakerService)
	{
        _speakerService = speakerService;
	}

	[HttpGet("all")]
	public async Task<IActionResult> GetAllSpeakersAsync([FromQuery] PageParams pageParams)
	{
		var result = await _speakerService.GetAllSpeakersAsync(pageParams);

		PageList<SpeakerDto> pagedSpeakers = (PageList<SpeakerDto>)result.Result;

		Response.AddPaginationHeader(pagedSpeakers.CurrentPage, pagedSpeakers.TotalPages,
			pagedSpeakers.PageSize, pagedSpeakers.TotalCount);


		return CustomResponse(result);
	}

	[HttpGet]
	public async Task<IActionResult> GetSpeakerAsync()
	{
        var userId = User.GetUserId();

		var result = await _speakerService.GetSpeakerByUserIdAsync(userId);

		if (!result.IsValid)
			return NotFound("Speaker not exist");

		return CustomResponse(result);
	}

	[HttpPost]
	public async Task<IActionResult> CreateSpeakerAsync(CreateSpeakerDto request)
	{
        var userId = User.GetUserId();
		await _speakerService.AddSpeakerAsync(userId, request);

		return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpeakerAsync(UpdateSpeakerDto request)
    {
        var userId = User.GetUserId();
        await _speakerService.UpdateSpeakerAsync(userId, request);

        return NoContent();
    }
}
