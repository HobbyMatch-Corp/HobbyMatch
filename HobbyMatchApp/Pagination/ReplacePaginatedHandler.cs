using HobbyMatch.Database.Common.Pagination;

namespace HobbyMatch.App.Pagination;

public class ReplacePaginatedHandler<T>: PaginatedQueryHandler<T>
{
    public ReplacePaginatedHandler(Func<PaginationParameters, Task<PaginatedData<T>>> fetchFunc, Action? onFetched, int pageSize = 10) : base(fetchFunc, onFetched, pageSize)
    {
    }

    protected override void ModifyCurrentData(PaginatedData<T> newData)
    {
        Data = newData.Data;
    }
}