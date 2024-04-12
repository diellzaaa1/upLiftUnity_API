using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.ApplicationRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository applicationRepository;
        public ApplicationController(IApplicationRepository _applicationRepository)
        {
            applicationRepository = _applicationRepository ??
            throw new ArgumentNullException(nameof(_applicationRepository));
        }
        [HttpGet]
        [Route("GetApplications")]

        public async Task<IActionResult> Get()
        {
            return Ok(await applicationRepository.GetSupVol_Applications());
        }
        [HttpGet]
        [Route("GetApplicationById/{id}")]
        public async Task<IActionResult> GetAppById(int id)    {
            return Ok(await applicationRepository.GetSupVol_ApplicationsById(id));
        }

        [HttpGet]
        [Route("GetApplicationByType")]
        public async Task<IActionResult> GetAppByType(String type)
        {
            return Ok(await applicationRepository.GetApplicationsByType(type));
        }

        [HttpPost]
        [Route("AddApplication")]
        public async Task<IActionResult> Post(SupVol_Applications app)
        {
            var result = await applicationRepository.InsertSupVol_Application(app);
            if (result.ApplicationId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateApplication")]
        public async Task<IActionResult> Put(SupVol_Applications app)
        {
            await applicationRepository.UpdateSupVol_Applications(app);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteApplication")]
        public JsonResult Delete(int id)
        {
            applicationRepository.DeleteApplication(id);
            return new JsonResult("Deleted Successfully");
        }
    }
}
