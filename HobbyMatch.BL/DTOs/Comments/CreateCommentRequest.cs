using System.ComponentModel.DataAnnotations;

namespace HobbyMatch.BL.DTOs.Comments;

public record CreateCommentRequest(int EventId, [MaxLength(300)] string Content);
