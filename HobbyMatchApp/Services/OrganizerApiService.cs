using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services
{
	public class OrganizerApiService : IOrganizerApiService
	{
		// TODO: get rid of redundant try catch blocks, fix them
		private readonly HttpClient _httpClient;

		private readonly Dictionary<Type, string> _endpointMap = new() // TODO: Create endpoint provider using strategy pattern
		{
			{ typeof(User), "users" },
			{ typeof(Organizer), "organizers" },
		};
		public OrganizerApiService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
		}
		private string GetEndpoint<T>() where T : BusinessClient
		{
			if (_endpointMap.TryGetValue(typeof(T), out var endpoint))
			{
				return endpoint;
			}
			throw new InvalidOperationException($"No endpoint declared for type: {typeof(T).Name}");
		}

		public async Task<T[]?> GetUsersAsync<T>() where T : BusinessClient
		{
			string endpoint = "";
			try
			{
				endpoint = GetEndpoint<T>();
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

		public async Task<T?> GetUserAsync<T>(int id) where T : BusinessClient
		{
			string endpoint = "";
			try
			{
				endpoint = GetEndpoint<T>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}

			T? user = null;
			var response = await _httpClient.GetAsync($"{endpoint}/ " + id);
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<T>();
			}
			return user;
		}

		public async Task<T?> EditUserAsync<T>(int id, T editedUser) where T : BusinessClient
		{
			string endpoint = "";
			try
			{
				endpoint = GetEndpoint<T>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}

			T? user = null;
			var response = await _httpClient.PostAsJsonAsync($"{endpoint}/" + id, editedUser);
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<T>();
			}
			return user;
		}
	}
}
