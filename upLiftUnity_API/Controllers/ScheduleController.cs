using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.ScheduleRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository scheduleRepository;
        public ScheduleController(IScheduleRepository _scheduleRepository)
        {
            scheduleRepository = _scheduleRepository ??
            throw new ArgumentNullException(nameof(_scheduleRepository));
        }
        [HttpGet]
        [Route("GetSchedules")]
       
        public async Task<IActionResult> Get()
        {
            return Ok(await scheduleRepository.GetSchedules());
        }
        [HttpGet]
        [Route("GetScheduleById/{id}")]
      

        public async Task<IActionResult> GetScheduleById(int id)
        {
            return Ok(await scheduleRepository.GetScheduleById(id));
        }
        [HttpGet]
        [Route("GetScheduleByUserId")]
        public async Task<IActionResult> GetScheduleByUserId(int userId)
        {
            return Ok(await scheduleRepository.GetScheduleByUserId(userId));
        }



        [HttpPost]
        [Route("AddSchedule")]

        public async Task<IActionResult> AddSchedule(Schedule sch)
        {
           
            var currentMonth = DateTime.Now.Month+1;
            var currentYear = DateTime.Now.Year;

          
            var existingSchedule = await scheduleRepository.GetScheduleByUserIdAndMonth(sch.UserId, currentMonth, currentYear);

            if (existingSchedule != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User already has a schedule for the current month. Cannot add a new schedule.");
            }
            else
            {
                var result = await scheduleRepository.InsertSchedule(sch);

                if (result.Id == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }

                return Ok("Schedule Added Successfully");
            }
        }
        [HttpPut]
        [Route("UpdateSchedule")]

        public async Task<IActionResult> Put(Schedule sch)
        {
            var updatedSchedule = await scheduleRepository.UpdateSchedule(sch) ;

            if (updatedSchedule == null)
            {
                return NotFound(); 
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteSchedule")]
        public JsonResult Delete(int id)
        {
            scheduleRepository.DeleteSchedule(id);
            return new JsonResult("Deleted Successfully");
        }

    }
}
