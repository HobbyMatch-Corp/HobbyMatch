using HobbyMatch.Database.Common.Pagination;

namespace HobbyMatch.App.Pagination;

public abstract class PaginatedQueryHandler<T>
{
    private readonly Action _onChanged;

    protected PaginatedQueryHandler(Action onChanged,
        int pageSize)
    {
        _onChanged = onChanged;
        PageSize = pageSize;
        CurrentPage = 1;
    }

    public List<T> Data { get; set; } = [];

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool IsLoading { get; set; }

    protected abstract Task<PaginatedData<T>> FetchPageAsync(PaginationParameters parameters);

    public async Task LoadPageAsync(int page)
    {
        if (IsLoading)
            return;

        IsLoading = true;

        _onChanged.Invoke();

        var result = await FetchPageAsync(new PaginationParameters(page, PageSize));

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

    // This can be modified if another way of modifying data (e.g appending) is added
    protected void ModifyCurrentData(PaginatedData<T> newData)
    {
        Data = newData.Data;
    }
}