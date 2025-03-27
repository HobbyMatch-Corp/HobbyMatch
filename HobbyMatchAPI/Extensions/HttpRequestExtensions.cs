namespace HobbyMatchAPI.Extensions;

public static class HttpRequestExtensions
{
    public static string? GetAccessToken(this HttpRequest request)
    {
        var authHeader = request.Headers.Authorization.FirstOrDefault();
        return authHeader?.StartsWith("Bearer") == true ? authHeader["Bearer ".Length..].Trim() : null;
    }

    public static string? GetRefreshToken(this HttpRequest request)
    {
        return request.Headers["Refresh-Token"].FirstOrDefault();
    }
}