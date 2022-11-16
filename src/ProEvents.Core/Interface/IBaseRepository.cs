namespace ProEvents.Core.Interface;
public interface IBaseRepository<T>
{
    //IQueryable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(T[] entities);
}
