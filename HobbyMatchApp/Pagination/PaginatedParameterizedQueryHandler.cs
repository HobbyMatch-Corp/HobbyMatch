using HobbyMatch.Database.Common.Pagination;

namespace HobbyMatch.App.Pagination;

public abstract class PaginatedParameterizedQueryHandler<T, TParams> : PaginatedQueryHandler<T> where TParams : class
{
    private readonly Func<TParams?, PaginationParameters, Task<PaginatedData<T>>> _fetchFunc;

    protected PaginatedParameterizedQueryHandler(
        Func<TParams?, PaginationParameters, Task<PaginatedData<T>>> fetchFunc,
        Action onChanged,
        int pageSize,
        TParams? initialParameters = null)
        : base(
            onChanged,
            pageSize)
    {
        QueryParams = initialParameters;
        _fetchFunc = fetchFunc;
    }

    protected TParams? QueryParams { get; private set; }

    protected override Task<PaginatedData<T>> FetchPageAsync(PaginationParameters paginationParams)
    {
        return _fetchFunc(QueryParams, paginationParams);
    }

    public async Task UpdateParametersAndReloadAsync(TParams newParameters)
    {
        QueryParams = newParameters;
        await LoadPageAsync(1);
    }
}