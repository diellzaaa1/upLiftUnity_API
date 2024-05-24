using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ActivitiesRepository
{
    public interface IActivitiesRepository
    {
        Task<IEnumerable<Conversation>> GetUserActivities();

        Task<IEnumerable<Conversation>> GetUserActivityById(int id);
    }
}
