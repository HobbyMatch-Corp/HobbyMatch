namespace HobbyMatch.Database.Common.Pagination;

// Record meant for returning data from a paginated endpoint
public record PaginatedData<T>(List<T> Data,int PageSize,int CurrentPage,int TotalCount)
{
    public int TotalPages => (int) Math.Ceiling((double) TotalCount / PageSize);
    public bool HasNext => CurrentPage < TotalPages;
}
