using HobbyMatch.BL.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.App.Services
{
	public interface IAuthApiService
	{
		Task<AuthResult?> LoginAsync(string email, string password);
		Task<HttpResponseMessage> RegisterUserAsync(string username, string email, string password);
		Task<HttpResponseMessage> RegisterBusinessClientAsync(string username, string email, string password, string taxId);

	}
}
