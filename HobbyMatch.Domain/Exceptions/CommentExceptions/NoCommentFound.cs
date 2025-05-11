namespace HobbyMatch.Domain.Exceptions.CommentExceptions;

public class NoCommentFound(int commentId) : Exception($"No comment with ${commentId} found");
