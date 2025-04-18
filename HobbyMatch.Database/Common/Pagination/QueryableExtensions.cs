using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Common.Pagination;

public static class QueryableExtensions
{
    // Use this method at the end of a query instead of ToListAsync to get paginated data
    public static async Task<PaginatedData<T>> ToPaginatedData<T>(this IQueryable<T> query,PaginationParameters paginationParams)
    {
        var totalCount = await query.CountAsync();

        var items = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).ToListAsync();

        return new PaginatedData<T>(Data: items, PageSize: paginationParams.PageSize, TotalCount: totalCount,CurrentPage: paginationParams.PageNumber);
    }
}