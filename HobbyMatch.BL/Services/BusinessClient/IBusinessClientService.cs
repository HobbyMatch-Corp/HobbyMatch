using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.BusinessClient
{
    public interface IBusinessClientService
    {
        public Task<List<Domain.Entities.BusinessClient>> GetBusinessClientsAsync();

        public Task<Domain.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task UpdateBusinessClientAsync(int userId, Domain.Entities.BusinessClient user);
    }
}
