using HobbyMatch.BL.Models.Auth;

namespace HobbyMatch.App.Services
{
	public interface IAuthApiService
	{
		Task<AuthResult?> LoginAsync(string email, string password);
		Task<string?> RegisterAsync(string email, string password);

	}
}
