using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.DTOs.Batchs;
using ProEvents.Application.DTOs.Events;
using ProEvents.Application.Interfaces;

namespace ProEvents.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class BatchController : MainController
{
    private readonly IBatchService _batchService;

    public BatchController(IBatchService batchService)
    {
        _batchService = batchService;
    }

    [HttpGet("{eventId:long}")]
    public async Task<IActionResult> GetAllBatchesByEventIdAsync(long eventId)
    {
        var batchesByEventId = await _batchService.GetAllBatchesByEventIdAsync(eventId);
        return CustomResponse(batchesByEventId);
    }

    [HttpGet("{id:long}/event/{eventId:long}")]
    public async Task<IActionResult> GetABatchByEventIdAsync(long eventId, long id)
    {
        var result = await _batchService.GetBatchByIdsAsync(eventId, id);

        if (!result.IsValid)
            return NotFound();

        return CustomResponse(result);
    }


    [HttpPut("{eventId:long}")]
    public async Task<IActionResult> SaveBatchesAsync(long eventId, SaveBatchDto[] request)
    {
        await _batchService.SaveBatchesAsync(eventId, request);
        return NoContent();
    }

    [HttpDelete("{id:long}/event/{eventId:long}")]
    public async Task<IActionResult> DeleteEventAsync(long eventId, long id)
    {
        var result = await _batchService.DeleteBatchAsync(eventId, id);

        if (!result.IsValid)
            return NotFound();

        return NoContent();
    }
}
