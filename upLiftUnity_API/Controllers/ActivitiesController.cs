using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.ActivitiesRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesRepository activitiesRepository;

        public ActivitiesController(IActivitiesRepository _activitiesRepository)
        {
            activitiesRepository = _activitiesRepository ??
            throw new ArgumentNullException(nameof(_activitiesRepository));
        }

        [HttpGet]
        [Route("GetActivities")]

        public async Task<IActionResult> Get()
        {
            return Ok(await activitiesRepository.GetUserActivities());
        }


        [HttpGet]
        [Route("GetActivitiesById/{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            return Ok(await activitiesRepository.GetUserActivityById(id));
        }


        [HttpGet]
        [Route("GetMonthlyLoginCounts")]
        public async Task<IActionResult> GetUserLoginCountsPerMonth()
        {
            var monthlyLoginCounts = await activitiesRepository.GetUserLoginCountsPerMonth();
            return Ok(monthlyLoginCounts);
        }

        [HttpGet]
        [Route("GetUserLoginCounts")]
        public async Task<IActionResult> GetUserLoginCounts()
        {
            var userLoginCounts = await activitiesRepository.GetUserLoginCounts();
            return Ok(userLoginCounts);
        }
    }
}
