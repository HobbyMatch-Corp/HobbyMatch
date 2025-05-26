using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Database.Repositories.BusinessClients;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.BusinessClients;

public class BusinessClientService : IBusinessClientService
{
    private readonly IBusinessClientRepository _businessClientRepository;

    public BusinessClientService(IBusinessClientRepository businessClientRepository)
    {
        _businessClientRepository = businessClientRepository;
    }

    public async Task<BusinessClient?> GetBusinessClientByIdAsync(int id)
    {
        return await _businessClientRepository.GetBusinessClientByIdAsync(id);
    }

    public async Task<BusinessClient?> GetBusinessClientByEmailAsync(string email)
    {
        return await _businessClientRepository.GetBusinessClientByEmailAsync(email);
    }

    public async Task<List<BusinessClient>> GetBusinessClientsAsync()
    {
        return await _businessClientRepository.GetBusinessClientsAsync();
    }

    public async Task<BusinessClient> UpdateBusinessClientAsync(
        int userId,
        UpdateBusinessClientDto businessClientDto
    )
    {
        var businessClient = new BusinessClient
        {
            UserName = businessClientDto.UserName,
            Email = businessClientDto.Email,
            TaxID = businessClientDto.TaxId,
        };
        return await _businessClientRepository.UpdateUserAsync(userId, businessClient);
    }
}
