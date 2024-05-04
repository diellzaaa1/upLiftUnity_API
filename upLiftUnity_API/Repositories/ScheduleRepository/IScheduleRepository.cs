using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ScheduleRepository
{
    public interface IScheduleRepository
    {
        Task<Schedule> InsertSchedule(Schedule objSch);

        Task<IEnumerable<Schedule>> GetSchedules();

        Task<Schedule> GetScheduleById(int id);


        Task<Schedule> UpdateSchedule(Schedule schedule);

        bool DeleteSchedule(int Id);

    }
}
