using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ScheduleRepository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly APIDbContext _appDBContext;

    public ScheduleRepository(APIDbContext context)
    {
        _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Schedule>> GetSchedules()
    {
        return await _appDBContext.Schedule
            .Include(schedule => schedule.UserSch).ToListAsync();
    }

    public async Task<Schedule> GetScheduleById(int id)
    {
        return await _appDBContext.Schedule.FindAsync(id);
    }
    public async Task<Schedule> GetScheduleByUserId(int userId)
    {
        return await _appDBContext.Schedule
            .Include(schedule => schedule.UserSch)
            .Where(schedule => schedule.UserId == userId)
            .OrderByDescending(schedule => schedule.Id) // Sigurohuni që keni një fushë për renditje të duhur
            .FirstOrDefaultAsync();
    }



    public async Task<Schedule> UpdateSchedule(Schedule sch)
    {
        _appDBContext.Entry(sch).State = EntityState.Modified;
        await _appDBContext.SaveChangesAsync();
        return sch;
    }


    public bool DeleteSchedule(int Id)
    {
        bool result = false;
        var app = _appDBContext.Schedule.Find(Id);
        if (app != null)
        {
            _appDBContext.Entry(app).State = EntityState.Deleted;
            _appDBContext.SaveChanges();
            result = true;
        }
        else
        {
            result = false;
        }
          return result;

    }

    public async Task<Schedule> InsertSchedule(Schedule objSch)
    {
        _appDBContext.Schedule.Add(objSch);
        await _appDBContext.SaveChangesAsync();
        return objSch;
    }
    public async Task<Schedule> GetScheduleByUserIdAndMonth(int userId, int month, int year)
    {
        try
        {
            var schedule = await _appDBContext.Schedule
                .FirstOrDefaultAsync(s => s.UserId == userId &&
                                          (s.FirstDate.Month == month && s.FirstDate.Year == year ||
                                           s.SecondDate.Month == month && s.SecondDate.Year == year ||
                                           s.ThirdDate.Month == month && s.ThirdDate.Year == year ||
                                           s.FourthDate.Month == month && s.FourthDate.Year == year));

            return schedule;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while getting schedule: {ex.Message}");
            return null; 
        }
    }

}
