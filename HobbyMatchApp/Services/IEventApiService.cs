namespace HobbyMatch.App.Services
{
    public interface IEventApiService
    {
        Task<bool?> EventSigninAsync(string EventId);
        Task<bool?> EventSignoutAsync(string EventId);
    }
}
