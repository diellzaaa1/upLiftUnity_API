using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using System.Linq;
using upLiftUnity_API.DTOs.ActivitiesDto;

namespace upLiftUnity_API.Repositories.ActivitiesRepository
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly APIDbContext _appDBContext;

        public ActivitiesRepository(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<UserActivity>> GetUserActivities()
        {
            return await _appDBContext.UserActivities
                .Include(a => a.User) 
                .ToListAsync();
        }


        public async Task<IEnumerable<UserActivity>> GetUserActivityById(int id)
        {
            return await _appDBContext.UserActivities
                .Where(activity => activity.UserId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserActivityMonthlyCountDto>> GetUserLoginCountsPerMonth()
        {
            var loginCountsPerMonth = await _appDBContext.UserActivities
                .GroupBy(activity => new { Month = activity.LoginTime.Month, Year = activity.LoginTime.Year })
                .Select(group => new UserActivityMonthlyCountDto
                {
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    LoginCount = group.Count()
                })
                .ToListAsync();

            return loginCountsPerMonth;
        }

        public async Task<IEnumerable<UserLoginCountDto>> GetUserLoginCounts()
        {
            var userLoginCounts = await _appDBContext.UserActivities
                .GroupBy(activity => activity.UserId)
                .Select(group => new UserLoginCountDto
                {
                    UserId = group.Key,
                    LoginCount = group.Count()
                })
                .ToListAsync();

            return userLoginCounts;
        }
    }
}
