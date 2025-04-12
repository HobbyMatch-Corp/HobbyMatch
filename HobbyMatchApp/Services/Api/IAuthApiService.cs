using HobbyMatch.BL.Models.Auth;

namespace HobbyMatch.App.Services.Api
{
	public interface IAuthApiService
	{
		Task<AuthResult?> LoginAsync(string email, string password);
		Task<HttpResponseMessage> RegisterUserAsync(string username, string email, string password);
		Task<HttpResponseMessage> RegisterBusinessClientAsync(string username, string email, string password, string taxId);

	}
}
