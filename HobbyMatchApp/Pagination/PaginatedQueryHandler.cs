using HobbyMatch.Database.Common.Pagination;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HobbyMatch.App.Pagination;

public abstract class PaginatedQueryHandler<T>
{
    public List<T> Data { get; set; } = [];
    
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool IsLoading { get; set; }
    private readonly Func<PaginationParameters, Task<PaginatedData<T>>> _fetchFunc;
    private readonly Action _onChanged;
    
    protected PaginatedQueryHandler(Func<PaginationParameters, Task<PaginatedData<T>>> fetchFunc,Action onChanged, int pageSize)
    {
        _fetchFunc = fetchFunc;
        _onChanged = onChanged;
        PageSize = pageSize;
        CurrentPage = 1;
    }

    public async Task LoadPageAsync(int page)
    {
        if (IsLoading)
            return;
        
        IsLoading = true;
        
        _onChanged.Invoke();
        
        var result = await _fetchFunc(new PaginationParameters(PageNumber: page,PageSize: PageSize));
        
        ModifyCurrentData(result);
        CurrentPage = result.CurrentPage;
        TotalPages = result.TotalPages;
        
        IsLoading = false;
        
        _onChanged.Invoke();
    }

    public async Task LoadNextPageAsync()
    {
        if (CurrentPage < TotalPages)
            await LoadPageAsync(CurrentPage + 1);
    }

    public async Task LoadPreviousPageAsync()
    {
        if (CurrentPage > 1)
            await LoadPageAsync(CurrentPage - 1);
    }

    protected abstract void ModifyCurrentData(PaginatedData<T> newData);
}