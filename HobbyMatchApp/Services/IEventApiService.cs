using HobbyMatch.BL.Models.Auth;

namespace HobbyMatch.App.Services
{
    public interface IEventApiService
    {
        Task<string?> EventSigninAsync(string EventId);
        Task<string?> EventSignoutAsync(string EventId);
    }
}
