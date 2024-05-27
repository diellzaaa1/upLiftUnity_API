using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.ScheduleRepository
{
    public interface IScheduleRepository
    {
        Task<Schedule> InsertSchedule(Schedule objSch);

        Task<IEnumerable<Schedule>> GetSchedules();

        Task<Schedule> GetScheduleById(int id);

        Task<Schedule> GetScheduleByUserId(int useriId);


        Task<Schedule> UpdateSchedule(Schedule schedule);

        bool DeleteSchedule(int Id);

        public  Task<Schedule> GetScheduleByUserIdAndMonth(int userId, int month, int year);

        
        
    }
}
