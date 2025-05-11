using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Exceptions.CommentExceptions;
using HobbyMatch.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.BL.Services.Comments;

public class CommentService : ICommentService
{
    private readonly AppDbContext _dbContext;

    public CommentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> GetEventCommentsAsync(int eventId)
    {
        return await _dbContext.Comments.Where(c => c.EventId == eventId).ToListAsync();
    }

    public async Task<Comment> CreateCommentAsync(CreateCommentRequest request, int userId)
    {
        var comment = new Comment
        {
            EventId = request.EventId,
            UserId = userId,
            Content = request.Content,
            CreatedAt = DateTime.Now,
        };
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteCommentAsync(int commentId, int userId)
    {
        var comment = await _dbContext.Comments.FindAsync(commentId);
        if (comment == null)
            throw new NoCommentFound(commentId);

        if (comment.UserId != userId)
            throw new UnauthorizedCommentDelete();

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }
}
