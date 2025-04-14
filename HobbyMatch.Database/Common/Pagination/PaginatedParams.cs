namespace HobbyMatch.Database.Common.Pagination;

//This should be included in every request to a controller with pagination
//Page numbers start from 1
public record PaginationParameters(int PageNumber,int PageSize);