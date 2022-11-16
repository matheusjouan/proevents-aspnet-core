using ProEvents.Application.DTOs.Batchs;
using ProEvents.Core.Models;

namespace ProEvents.Application.Interfaces;
public interface IBatchService
{
    Task<CustomResult> GetAllBatchesByEventIdAsync(long eventId);
    Task<CustomResult> GetBatchByIdsAsync(long eventId, long id);
    Task<CustomResult> SaveBatchesAsync(long eventId, SaveBatchDto[] batches);
    Task<CustomResult> DeleteBatchAsync(long eventId, long id);
}
