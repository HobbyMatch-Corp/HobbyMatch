using HobbyMatch.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.BusinessClient
{
    public interface IBusinessClientService
    {
        public Task<List<Model.Entities.BusinessClient>> GetBusinessClientsAsync();

        public Task<Model.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task UpdateBusinessClientAsync(int userId, Model.Entities.BusinessClient user);
    }
}
