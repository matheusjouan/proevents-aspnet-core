using ProEvents.Core.Interface;
using ProEvents.Infrastructure.Persistence.Context;

namespace ProEvents.Infrastructure.Persistence.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class 
{
    private readonly ProEventsContext _context;
    public BaseRepository(ProEventsContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(T[] entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}
