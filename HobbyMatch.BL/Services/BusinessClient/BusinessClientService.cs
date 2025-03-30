using HobbyMatch.Database.Repositories.AppUser;
using HobbyMatch.Database.Repositories.BusinessClient;
using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.BusinessClient
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

        public async Task<List<Domain.Entities.BusinessClient>> GetBusinessClientsAsync()
        {
            return await _businessClientRepository.GetBusinessClientsAsync();
        }

        public async Task UpdateBusinessClientAsync(int userId, Domain.Entities.BusinessClient businessClient)
        {
            await _businessClientRepository.UpdateUserAsync(userId, businessClient);
        }
    }
}
