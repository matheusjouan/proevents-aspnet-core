
using Microsoft.EntityFrameworkCore;
using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Infrastructure.Persistence.Context;

namespace ProEvents.Infrastructure.Persistence.Repositories;
public class SocialNetworkRepository : BaseRepository<SocialNetwork>, ISocialNetworkRepository
{
    private readonly ProEventsContext _context;

    public SocialNetworkRepository(ProEventsContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SocialNetwork>> GetAllSocialNetworkByEventAsync(long eventId)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks
            .Where(s => s.EventId == eventId);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<SocialNetwork>> GetAllSocialNetworkSpeakersAsync(long speakerId)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks
            .Where(s => s.SpeakerId == speakerId);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<SocialNetwork> GetSocialNetworkEventByIdsAsync(long eventId, long id)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks
            .Where(s => s.EventId == eventId && s.Id == id);

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<SocialNetwork> GetSocialNetworkSpeakerByIdsAsync(long speakerId, long id)
    {
        IQueryable<SocialNetwork> query = _context.SocialNetworks
            .Where(s => s.SpeakerId == speakerId && s.Id == id);

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }
}
