using System.ComponentModel.DataAnnotations;

namespace HobbyMatch.Domain.Requests;

public record CreateCommentRequest(int EventId, [MaxLength(300)] string Content);
