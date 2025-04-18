using HobbyMatch.Database.Common.Pagination;

namespace HobbyMatch.App.Pagination;

public class ReplacePaginatedHandler<T>: PaginatedQueryHandler<T>
{
    public ReplacePaginatedHandler(Func<PaginationParameters, Task<PaginatedData<T>>> fetchFunc, Action onChanged, int pageSize = 10) : base(fetchFunc, onChanged, pageSize)
    {
    }

    protected override void ModifyCurrentData(PaginatedData<T> newData)
    {
        Data = newData.Data;
    }
}