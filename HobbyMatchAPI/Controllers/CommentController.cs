using System.Security.Claims;
using HobbyMatch.BL.DTOs.Comments;
using HobbyMatch.BL.Services.Comments;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetEventComments([FromRoute] int eventId)
    {
        var comments = await _commentService.GetEventCommentsAsync(eventId);
        return Ok(comments);
    }

    [Authorize]
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _commentService.DeleteCommentAsync(commentId, userId);
        return NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var comment = await _commentService.CreateCommentAsync(request, userId);
        return Ok(comment.ToDto());
    }
}
