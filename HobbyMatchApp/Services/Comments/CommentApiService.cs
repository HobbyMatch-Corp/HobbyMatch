using HobbyMatch.BL.DTOs.Comments;

namespace HobbyMatch.App.Services.Comments;

public class CommentApiService : ICommentApiService
{
    private readonly HttpClientUtils _httpClientUtils;
    public CommentApiService(HttpClientUtils httpClientUtils)
    {
        _httpClientUtils = httpClientUtils;
    }

    public async Task<CommentDto?> CreateComment(int eventId, string content)
    {
        var commentRequest = new CreateCommentRequest(eventId, content);
        return await _httpClientUtils.PostAsyncSafe<CreateCommentRequest, CommentDto>("comment", commentRequest);
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        return await _httpClientUtils.DeleteAsyncSafe($"comment/{commentId}");
    }

    public async Task<List<CommentDto>?> GetComments(int eventId)
    {
        return await _httpClientUtils.GetAsyncSafe<List<CommentDto>>($"comment/event/{eventId}");
    }
}
