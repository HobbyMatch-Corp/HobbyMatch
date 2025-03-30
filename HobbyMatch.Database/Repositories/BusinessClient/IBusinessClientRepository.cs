namespace HobbyMatch.Database.Repositories.BusinessClient
{
    public interface IBusinessClientRepository
    {
        public Task<List<Model.Entities.BusinessClient>> GetBusinessClientsAsync();

        public Task<Model.Entities.BusinessClient?> GetBusinessClientByIdAsync(int id);

        public Task UpdateUserAsync(int businessClientId, Model.Entities.BusinessClient businessClient);
    }
}
