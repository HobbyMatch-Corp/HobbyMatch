using HobbyMatch.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.BusinessClients
{
    public class BusinessClientRepository : IBusinessClientRepository
    {
        private readonly AppDbContext _dbContext;

        public BusinessClientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id)
        {
            return await _dbContext.BusinessClients.FindAsync(id);
        }

        public async Task<List<Domain.Entities.BusinessClient>> GetBusinessClientsAsync()
        {
            return await _dbContext.BusinessClients.ToListAsync();
        }

        public async Task UpdateUserAsync(int businessClientId, Domain.Entities.BusinessClient businessClient)
        {
            var dbBusinessClient = await GetBusinessClientByIdAsync(businessClientId);

            if (dbBusinessClient != null)
            {
                dbBusinessClient.Email = businessClient.Email;
                dbBusinessClient.UserName = businessClient.UserName;
                dbBusinessClient.TaxID = businessClient.TaxID;
            }
        }
    }
}
