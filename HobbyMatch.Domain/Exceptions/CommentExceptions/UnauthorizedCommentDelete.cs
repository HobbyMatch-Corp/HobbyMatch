namespace HobbyMatch.Domain.Exceptions.CommentExceptions;

public class UnauthorizedCommentDelete()
    : Exception("Cannot delete comment that belongs to another user");
