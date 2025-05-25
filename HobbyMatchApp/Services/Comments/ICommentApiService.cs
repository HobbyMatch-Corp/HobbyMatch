using HobbyMatch.BL.DTOs.Comments;

namespace HobbyMatch.App.Services.Comments;

public interface ICommentApiService
{
    public Task<CommentDto?> CreateComment(int eventId, string content);
    public Task<bool> DeleteComment(int commentId);
    public Task<List<CommentDto>?> GetComments(int eventId);
}
