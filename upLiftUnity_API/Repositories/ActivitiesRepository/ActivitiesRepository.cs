using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ActivitiesRepository
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly APIDbContext _appDBContext;

        public ActivitiesRepository(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Conversation>> GetUserActivities()
        {
            return await _appDBContext.UserActivities
                .Include(a => a.User) 
                .ToListAsync();
        }


        public async Task<IEnumerable<Conversation>> GetUserActivityById(int id)
        {
            return await _appDBContext.UserActivities
                .Where(activity => activity.UserId == id)
                .ToListAsync();
        }
    }
}
