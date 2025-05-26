using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.BusinessClients;

public interface IBusinessClientService
{
    public Task<List<BusinessClient>> GetBusinessClientsAsync();

    public Task<BusinessClient?> GetBusinessClientByIdAsync(int id);

    public Task<BusinessClient> UpdateBusinessClientAsync(
        int userId,
        UpdateBusinessClientDto businessClientDto
    );
    public Task<BusinessClient?> GetBusinessClientByEmailAsync(string emailJwt);
}
