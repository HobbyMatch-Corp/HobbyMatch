using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services.Api
{
	public class OrganizerApiService : IOrganizerApiService
	{
		// TODO: get rid of redundant try catch blocks, fix them
		private readonly HttpClient _httpClient;
		private readonly EndpointProvider _endpointProvider;
		public OrganizerApiService(IHttpClientFactory httpClientFactory, EndpointProvider endpointProvider)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
            _endpointProvider = endpointProvider;	
		}

		public async Task<T?> GetMe<T>() where T: OrganizerDto
		{
			string endpoint = "";
			try
			{
				endpoint = _endpointProvider.GetEndpoint<T>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
			T? user = null;
			var response = await _httpClient.GetAsync($"{endpoint}/me");
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<T>();
			}

			return user;
		}

		public async Task<T[]?> GetUsersAsync<T>() where T : OrganizerDto
		{
			string endpoint = "";
			try
			{
				endpoint = _endpointProvider.GetEndpoint<T>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
			T[]? user = null;
			var response = await _httpClient.GetAsync(endpoint);
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<T[]>();
			}

			return user;

		}

		public async Task<T?> GetUserAsync<T>(int id) where T : OrganizerDto
		{
			string endpoint = "";
			try
			{
				endpoint = _endpointProvider.GetEndpoint<T>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}

			T? user = null;
			var response = await _httpClient.GetAsync($"{endpoint}/{id}");
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<T>();
			}
			return user;
		}

		public async Task<bool> UpdateUserAsync<T, TDto>(string id, TDto editedUser) where T : OrganizerDto
		{
			string endpoint = "";
			try
			{
				endpoint = _endpointProvider.GetEndpoint<T>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			T? user = null;
			var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{id}", editedUser);
			return response.IsSuccessStatusCode ? true : false;
		}
    }
}
