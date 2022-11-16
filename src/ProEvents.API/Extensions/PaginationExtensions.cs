using System.Text.Json;

namespace ProEvents.API.Extensions;

public static class PaginationExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, int currentPage,
        int totalPages, int pageSize, int totalItems)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        response.Headers.Add("X-Pagination", JsonSerializer.Serialize(
            new
            {
                currentPage = currentPage,
                totalPages = totalPages,
                pageSize = pageSize,
                totalItems = totalItems
            }, options));
    }
}
