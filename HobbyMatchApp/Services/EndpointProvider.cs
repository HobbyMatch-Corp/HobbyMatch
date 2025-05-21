using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services
{
	public class EndpointProvider
	{
		private readonly Dictionary<Type, string> _endpointMap = new() 
		{
			{ typeof(UserDto), "users" },
			{ typeof(BusinessClientDto), "businessClients" },
		};
		public string GetEndpoint<T>()  where T : OrganizerDto
		{
			if (_endpointMap.TryGetValue(typeof(T), out var endpoint))
			{
				return endpoint;
			}
			throw new InvalidOperationException($"No endpoint declared for type: {typeof(T).Name}");
		}
	}
}
