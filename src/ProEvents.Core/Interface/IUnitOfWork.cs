using ProSpeakers.Core.Interface;

namespace ProEvents.Core.Interface;
public interface IUnitOfWork
{
    public IEventRepository EventRepository { get; }
    public IBatchRepository BatchRepository { get; }
    public ISpeakerRepository SpeakerRepository { get; }
    public ISocialNetworkRepository SocialNetworkRepository { get; }
    Task SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    void Dispose();
}
