using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Core.Models;

namespace ProSpeakers.Core.Interface;
public interface ISpeakerRepository : IBaseRepository<Speaker>
{
    Task<PageList<Speaker>> GetAllSpeakersAsync(PageParams pageParams, bool includeEvent = false);
    Task<Speaker> GetSpeakeByIdAsync(long userId, bool includeEvent = false);
}
