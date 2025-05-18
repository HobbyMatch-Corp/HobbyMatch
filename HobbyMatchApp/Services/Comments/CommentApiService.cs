using HobbyMatch.BL.DTOs.Comments;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Comments;

public class CommentApiService : ICommentApiService
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unauthorizedClient;

    public CommentApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
        _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");
    }

    public async Task<CommentDto?> CreateComment(int eventId, string content)
    {
        var uri = "comments";

        var commentRequest = new CreateCommentRequest(eventId, content);

        var response = await _httpClient.PostAsJsonAsync(uri, commentRequest);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CommentDto>();

        return null;
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        var uri = $"comments/{commentId}";

        var response = await _httpClient.DeleteAsync(uri);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<CommentDto>?> GetComments(int eventId)
    {
        var uri = $"comments/event/{eventId}";

        var response = await _httpClient.GetFromJsonAsync<List<CommentDto>>(uri);

        return response;
    }
}
