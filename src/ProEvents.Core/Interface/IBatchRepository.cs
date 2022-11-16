using ProEvents.Core.Entities;

namespace ProEvents.Core.Interface;
public interface IBatchRepository : IBaseRepository<Batch>
{
    Task<IEnumerable<Batch>> GetAllBatchesByEventIdAsync(long eventId);
    Task<Batch> GetBatchByIdsAsync(long eventId, long id);
}
