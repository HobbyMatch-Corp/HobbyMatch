namespace HobbyMatch.Database.Common.Pagination;

public static class PaginatedDataExtensions
{
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