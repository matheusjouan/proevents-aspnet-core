
using Microsoft.EntityFrameworkCore;
using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Core.Models;
using ProEvents.Infrastructure.Persistence.Context;

namespace ProEvents.Infrastructure.Persistence.Repositories;
public class EventRepository : BaseRepository<Event>, IEventRepository
{
    private readonly ProEventsContext _context;
    public EventRepository(ProEventsContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PageList<Event>> GetAllEventsAsync(long userId, PageParams pageParams, bool includeSpeaker = false)
    {
        IQueryable<Event> query = _context.Events
            .AsNoTracking()
            .Where(e => e.Theme.ToLower().Contains(pageParams.SearchFilter.ToLower()) && e.UserId == userId)
            .Include(e => e.Batches)
            .Include(e => e.SocialNetworks);

        if (includeSpeaker)
            IncludeSpeakerInQuery(query);

        return await PageList<Event>.ToPagedList(query, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Event> GetEventByIdAsync(long userId, long id, bool includeSpeaker = false)
    {

        IQueryable<Event> query = _context.Events
            .Where(e => e.Id == id && e.UserId == userId)
            .Include(e => e.Batches)
            .Include(e => e.SocialNetworks);

        if (includeSpeaker)
            IncludeSpeakerInQuery(query);

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }
    private IQueryable<Event> IncludeSpeakerInQuery(IQueryable<Event> query)
    {
        return query = query.Include(e => e.SpeakersEvents)
            .ThenInclude(se => se.Speaker);
    }
}
