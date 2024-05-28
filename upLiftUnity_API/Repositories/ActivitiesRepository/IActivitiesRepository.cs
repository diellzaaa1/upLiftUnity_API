using upLiftUnity_API.DTOs.ActivitiesDto;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ActivitiesRepository
{
    public interface IActivitiesRepository
    {
        Task<IEnumerable<UserActivity>> GetUserActivities();

        Task<IEnumerable<UserActivity>> GetUserActivityById(int id);
        Task<IEnumerable<UserActivityMonthlyCountDto>> GetUserLoginCountsPerMonth();
        Task<IEnumerable<UserLoginCountDto>> GetUserLoginCounts();



    }
}
