using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.RevistaRepository
{
    public interface IRevistaRepository
    {
        Task CreateRevista(string magazineName, int issueNumber, int botuesiId);

        Task<List<Revista>> GetAllRevistat();

      
    }
}
