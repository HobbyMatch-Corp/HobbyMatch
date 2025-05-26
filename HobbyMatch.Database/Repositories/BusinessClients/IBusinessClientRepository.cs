using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.BusinessClients
{
    public interface IBusinessClientRepository
    {
        public Task<List<BusinessClient>> GetBusinessClientsAsync();

        public Task<BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task<BusinessClient> UpdateUserAsync(
            int businessClientId,
            BusinessClient businessClient
        );
        public Task<BusinessClient?> GetBusinessClientByEmailAsync(string email);
    }
}
