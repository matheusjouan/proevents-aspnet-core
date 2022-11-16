using ProEvents.Core.Entities;
using ProEvents.Core.Models;

namespace ProEvents.Core.Interface;
public interface IEventRepository : IBaseRepository<Event>
{
    Task<PageList<Event>> GetAllEventsAsync(long userId, PageParams pageParams, bool includeSpeaker = false);
    Task<Event> GetEventByIdAsync(long userId, long id, bool includeSpeaker = false);
}
