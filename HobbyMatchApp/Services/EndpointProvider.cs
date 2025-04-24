using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services
{
	public class EndpointProvider
	{
		private readonly Dictionary<Type, string> _endpointMap = new() 
		{
			{ typeof(User), "users" },
			{ typeof(BusinessClient), "businessClients" },
		};
		public string GetEndpoint<T>()  where T : Organizer
		{
			if (_endpointMap.TryGetValue(typeof(T), out var endpoint))
			{
				return endpoint;
			}
			throw new InvalidOperationException($"No endpoint declared for type: {typeof(T).Name}");
		}
	}
}
