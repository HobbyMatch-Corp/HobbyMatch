namespace HobbyMatch.Database.Common.Pagination;

public static class PaginatedDataExtensions
{
    // Use this method for mapping items (e.g for converting items to DTOs) without manually creating a new PaginatedData
    // object
    public static PaginatedData<TResult> MapItems<TSource, TResult>(
        this PaginatedData<TSource> source,
        Func<TSource, TResult> selector)
    {
        var mappedData = source.Data.Select(selector).ToList();

        return new PaginatedData<TResult>(
            mappedData,
            source.PageSize,
            source.TotalCount,
            source.CurrentPage
        );
    }
}