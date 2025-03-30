namespace HobbyMatch.BL.Services.BusinessClient
{
    public interface IBusinessClientService
    {
        public Task<List<Model.Entities.BusinessClient>> GetBusinessClientsAsync();

        public Task<Model.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task UpdateBusinessClientAsync(int userId, Model.Entities.BusinessClient user);
    }
}
