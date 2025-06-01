using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services.Api
{
	public class OrganizerApiService : IOrganizerApiService
    {
        private readonly HttpClientUtils _httpClientUtils;
        private readonly EndpointProvider _endpointProvider;

        public OrganizerApiService(HttpClientUtils httpClientUtils, EndpointProvider endpointProvider)
        {
            _httpClientUtils = httpClientUtils;
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

			return await _httpClientUtils.GetAsyncSafe<T>($"{endpoint}/me");
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

            return await _httpClientUtils.GetAsyncSafe<T[]?>(endpoint);

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

            return await _httpClientUtils.GetAsyncSafe<T?>($"{endpoint}/{id}");
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

			return await _httpClientUtils.PutAsyncSafe<TDto, bool>($"{endpoint}/{id}", editedUser, returnStatus: true);
        }
    }
}
