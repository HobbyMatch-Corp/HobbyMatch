using HobbyMatch.Database.Data;
using HobbyMatch.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Database.Repositories.BusinessClient
{
    public class BusinessClientRepository : IBusinessClientRepository
    {
        private readonly AppDbContext _dbContext;

        public BusinessClientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Model.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id)
        {
            return await _dbContext.BusinessClients.FindAsync(id);
        }

        public async Task<List<Model.Entities.BusinessClient>> GetBusinessClientsAsync()
        {
            return await _dbContext.BusinessClients.ToListAsync();
        }

        public async Task UpdateUserAsync(int businessClientId, Model.Entities.BusinessClient businessClient)
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
