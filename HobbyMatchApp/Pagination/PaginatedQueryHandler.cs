using HobbyMatch.Database.Common.Pagination;

namespace HobbyMatch.App.Pagination;

public abstract class PaginatedQueryHandler<T>
{
    public List<T> Data { get; set; } = [];
    
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool IsLoading { get; set; }
    private readonly Func<PaginationParameters, Task<PaginatedData<T>>> _fetchFunc;
    private readonly Action? _onFetched;
    
    protected PaginatedQueryHandler(Func<PaginationParameters, Task<PaginatedData<T>>> fetchFunc,Action? onFetched, int pageSize)
    {
        _fetchFunc = fetchFunc;
        _onFetched = onFetched;
        PageSize = pageSize;
        CurrentPage = 1;
    }

    public async Task LoadPageAsync(int page)
    {
        if (IsLoading)
            return;
        
        IsLoading = true;
        
        _onFetched?.Invoke();
        
        var result = await _fetchFunc(new PaginationParameters(PageNumber: CurrentPage,PageSize: PageSize));
        
        Data.AddRange(result.Data);
        CurrentPage = result.CurrentPage;
        TotalPages = result.TotalPages;
        
        IsLoading = false;
        
        _onFetched?.Invoke();
    }

    public async Task LoadNextPageAsync()
    {
        if (CurrentPage < TotalPages)
            await LoadPageAsync(CurrentPage);
    }

    public async Task LoadPreviousPageAsync()
    {
        if (CurrentPage > 1)
            await LoadPageAsync(CurrentPage - 1);
    }

    protected abstract void ModifyCurrentData(PaginatedData<T> newData);
}