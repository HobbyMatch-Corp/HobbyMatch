using HobbyMatch.BL.DTOs.Comments;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Comments;

public class CommentApiService : ICommentApiService
{
    private readonly HttpClient _httpClient;

    public CommentApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
    }

    public async Task<CommentDto?> CreateComment(int eventId, string content)
    {
        var uri = "comment";

        var commentRequest = new CreateCommentRequest(eventId, content);

        var response = await _httpClient.PostAsJsonAsync(uri, commentRequest);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CommentDto>();

        return null;
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        var uri = $"comment/{commentId}";

        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<CommentDto>?> GetComments(int eventId)
    {
        var uri = $"comment/event/{eventId}";

        var response = await _httpClient.GetFromJsonAsync<List<CommentDto>>(uri);

        return response;
    }
}
