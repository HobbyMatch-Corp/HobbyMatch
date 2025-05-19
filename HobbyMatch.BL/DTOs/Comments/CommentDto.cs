using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Comments;

public record CommentDto(int Id, string Content, int UserId, string Username, DateTime CreatedAt);

public static class CommentExtensions
{
    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto(
            comment.Id,
            comment.Content,
            comment.OrganizerId,
            comment.Organizer.UserName,
            comment.CreatedAt
        );
    }
}
