using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Database.Repositories.BusinessClients;

namespace HobbyMatch.BL.Services.BusinessClients
{
    public class BusinessClientService : IBusinessClientService
    {
        private readonly IBusinessClientRepository _businessClientRepository;

        public BusinessClientService(IBusinessClientRepository businessClientRepository)
        {
            _businessClientRepository = businessClientRepository;
        }

        public async Task<Domain.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id)
        {
            return await _businessClientRepository.GetBusinessClientByIdAsync(id);
        }
        public async Task<Domain.Entities.BusinessClient?> GetBusinessClientByEmailAsync(string email)
        {
            return await _businessClientRepository.GetBusinessClientByEmailAsync(email);
        }

        public async Task<List<Domain.Entities.BusinessClient>> GetBusinessClientsAsync()
        {
            return await _businessClientRepository.GetBusinessClientsAsync();
        }

        public async Task UpdateBusinessClientAsync(int userId, UpdateBusinessClientDto businessClientDto)
        {
            var businessClient = new Domain.Entities.BusinessClient
            {
                UserName = businessClientDto.UserName,
                Email = businessClientDto.Email,
                TaxID = businessClientDto.TaxId,
            };
            await _businessClientRepository.UpdateUserAsync(userId, businessClient);
        }
    }
}
