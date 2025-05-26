using HobbyMatch.BL.DTOs.Comments;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Comments;

public interface ICommentService
{
    public Task<List<Comment>> GetEventCommentsAsync(int eventId);
    public Task<Comment> CreateCommentAsync(CreateCommentRequest request, int userId);

    public Task DeleteCommentAsync(int commentId, int userId);
}
