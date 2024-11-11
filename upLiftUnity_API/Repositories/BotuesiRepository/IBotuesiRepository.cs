using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.BotuesiRepository
{
    public interface IBotuesiRepository
    {
        Task CreateBotuesi(string publisherName, string location);

        Task<List<Botuesi>> GetAllBotuesit();

        Task<Botuesi> GetBotuesiById(int Id);

        Task UpdateBotuesi(int id, string publisherName, string location);
    }
}
