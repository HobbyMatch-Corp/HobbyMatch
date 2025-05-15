using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.BusinessClients
{
    public interface IBusinessClientService
    {
        public Task<List<Domain.Entities.BusinessClient>> GetBusinessClientsAsync();

        public Task<Domain.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task UpdateBusinessClientAsync(int userId, UpdateBusinessClientDto businessClientDto);
		public Task<BusinessClient?> GetBusinessClientByEmailAsync(string emailJwt);
	}
}
