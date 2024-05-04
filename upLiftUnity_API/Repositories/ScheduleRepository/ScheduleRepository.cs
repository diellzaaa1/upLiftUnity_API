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
        return await _appDBContext.Schedule.ToListAsync();
    }

    public async Task<Schedule> GetScheduleById(int id)
    {
        return await _appDBContext.Schedule.FindAsync(id);
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


}
