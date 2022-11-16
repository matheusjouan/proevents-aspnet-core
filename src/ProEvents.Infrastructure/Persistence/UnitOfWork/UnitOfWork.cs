using Microsoft.EntityFrameworkCore.Storage;
using ProEvents.Core.Exceptions;
using ProEvents.Core.Interface;
using ProEvents.Infrastructure.Persistence.Context;
using ProSpeakers.Core.Interface;

namespace ProEvents.Infrastructure.Persistence.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ProEventsContext _context;
    private IDbContextTransaction _transaction;
    private bool _disposed;

    public UnitOfWork(IEventRepository eventRepository,
        ProEventsContext context, IBatchRepository batchRepository, 
        ISpeakerRepository speakerRepository, ISocialNetworkRepository socialnetworkRepository)
    {
        EventRepository = eventRepository;
        BatchRepository = batchRepository;
        SpeakerRepository = speakerRepository;
        SocialNetworkRepository = socialnetworkRepository;
        _context = context;
    }

    public IEventRepository EventRepository { get; }
    public IBatchRepository BatchRepository { get; }
    public ISpeakerRepository SpeakerRepository { get; }
    public ISocialNetworkRepository SocialNetworkRepository { get; }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        } catch(TransactionException)
        {
            await _transaction.RollbackAsync();
            throw new TransactionException();
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();

        _disposed = true;
    }
}
