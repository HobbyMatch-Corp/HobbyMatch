using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.BusinessClients;

public class BusinessClientRepository : IBusinessClientRepository
{
    private readonly AppDbContext _dbContext;

    public BusinessClientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BusinessClient?> GetBusinessClientByIdAsync(int id)
    {
        return await _dbContext.BusinessClients.FindAsync(id);
    }

    public async Task<BusinessClient?> GetBusinessClientByEmailAsync(string email)
    {
        return await _dbContext.BusinessClients.FirstOrDefaultAsync(b => b.Email == email);
    }

    public async Task<List<BusinessClient>> GetBusinessClientsAsync()
    {
        return await _dbContext.BusinessClients.ToListAsync();
    }

    public async Task<BusinessClient> UpdateUserAsync(
        int businessClientId,
        BusinessClient businessClient
    )
    {
        var dbBusinessClient = await GetBusinessClientByIdAsync(businessClientId);

        if (dbBusinessClient != null)
        {
            dbBusinessClient.Email = businessClient.Email;
            dbBusinessClient.UserName = businessClient.UserName;
            dbBusinessClient.TaxID = businessClient.TaxID;
        }
        else
        {
            throw new DbUpdateException("The client to be updated was not found");
        }

        await _dbContext.SaveChangesAsync();
        return dbBusinessClient;
    }
}
