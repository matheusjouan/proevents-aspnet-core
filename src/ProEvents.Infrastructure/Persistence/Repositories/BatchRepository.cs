using Microsoft.EntityFrameworkCore;
using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Infrastructure.Persistence.Context;

namespace ProEvents.Infrastructure.Persistence.Repositories;
public class BatchRepository : BaseRepository<Batch>, IBatchRepository
{
    private readonly ProEventsContext _context;

	public BatchRepository(ProEventsContext context) : base(context) 
    {
        _context = context; 
    }

    public async Task<IEnumerable<Batch>> GetAllBatchesByEventIdAsync(long eventId)
    {
        IQueryable<Batch> query = _context.Batches
            .Where(b => b.EventId == eventId);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<Batch> GetBatchByIdsAsync(long eventId, long id)
    {
        IQueryable<Batch> query = _context.Batches
            .Where(b => b.EventId == eventId && b.Id == id);

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }
}
