using System.Security.Claims;

namespace HobbyMatch.App.Auth.TokenService
{
	/// <summary>
	/// This interface is responsible for managing the access token
	/// </summary>
	public interface ITokenService
	{
		Task SetAccessTokenAsync(string token);
		Task<string?> GetAccessTokenAsync();
		Task ClearAccessTokenAsync();
		Task<IEnumerable<Claim>> GetClaimsFromTokenAsync();
	}
}
