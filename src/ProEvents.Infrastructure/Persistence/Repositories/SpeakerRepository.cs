using Microsoft.EntityFrameworkCore;
using ProEvents.Core.Entities;
using ProEvents.Core.Identity.Enum;
using ProEvents.Core.Models;
using ProEvents.Infrastructure.Persistence.Context;
using ProSpeakers.Core.Interface;

namespace ProEvents.Infrastructure.Persistence.Repositories;
public class SpeakerRepository : BaseRepository<Speaker>, ISpeakerRepository
{
    private readonly ProEventsContext _context;

    public SpeakerRepository(ProEventsContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PageList<Speaker>> GetAllSpeakersAsync(PageParams pageParams, bool includeEvent = false)
    {
        IQueryable<Speaker> query = _context.Speakers
            .AsNoTracking()
            .Include(s => s.SocialNetworks)
            .Include(s => s.User);

        if (includeEvent)
            IncludeEventInQuery(query);

        query = query
            .Where(s => (s.Resume.ToLower().Contains(pageParams.SearchFilter.ToLower())) ||
                        (s.User.FirstName.ToLower().Contains(pageParams.SearchFilter.ToLower())) ||
                        (s.User.LastName.ToLower().Contains(pageParams.SearchFilter.ToLower())) &&
                        (s.User.Type == UserType.Speaker)
            );

        query = query
            .Where(s => (s.Resume.ToLower().Contains(pageParams.SearchFilter.ToLower())) ||
                        (s.User.FirstName.ToLower().Contains(pageParams.SearchFilter.ToLower())) ||
                        (s.User.LastName.ToLower().Contains(pageParams.SearchFilter.ToLower()))
            );

        return await PageList<Speaker>.ToPagedList(query, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Speaker> GetSpeakeByIdAsync(long userId, bool includeEvent = false)
    {
        IQueryable<Speaker> query = _context.Speakers
            .Where(s => s.User.Id == userId)
            .Include(s => s.SocialNetworks)
            .Include(s => s.User);

        if (includeEvent)
            IncludeEventInQuery(query);

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    private IQueryable<Speaker> IncludeEventInQuery(IQueryable<Speaker> query)
    {
        return query.Include(s => s.SpeakersEvents)
            .ThenInclude(se => se.Event);
    }
}
