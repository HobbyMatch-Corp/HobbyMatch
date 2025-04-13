namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool?> EventSigninAsync(string EventId);
        Task<bool?> EventSignoutAsync(string EventId);
    }
}
