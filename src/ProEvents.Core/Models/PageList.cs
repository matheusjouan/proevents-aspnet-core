using Microsoft.EntityFrameworkCore;

namespace ProEvents.Core.Models;
public class PageList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    // Usado p/ AutoMapper
    public PageList() { }
    public PageList(List<T> items, int count, int pageNumber, int pageSize) 
    {
        TotalCount = count;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static async Task<PageList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageList<T>(items, count, pageNumber, pageSize);
    }
}
