namespace HobbyMatch.Database.Common.Pagination;

//This should be included in every request to an endpoint with pagination
//Page numbers start from 1
public record PaginationParameters(int PageNumber,int PageSize);