using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.PlanetRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/satelite")]
    [ApiController]
    public class SateliteController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly IPlanetRepo _planet;


        public SateliteController(APIDbContext _dbcontext, IPlanetRepo planet)
        {
            _context = _dbcontext;
            _planet = planet;

        }

        [HttpPost]
        [Route("CreatePlanet")]

        public async Task<IActionResult> CreatePlanet([FromBody] Planet planet)
        {
            if (planet == null)
            {
                return BadRequest("Planet is null");

            }

            var createdPlanet = await _planet.CreatePlanet(planet);
            return Ok(createdPlanet);
        }


        [HttpGet]
        [Route("GetPlanets")]

        public async Task<IActionResult> Get()
        {
            var planets = await _planet.GetPlanets();
            return Ok(planets);
        }


        [HttpPut]
        [Route("UpdatePlanet")]

        public async Task<IActionResult> Put(string name,string type)
        {
            await _planet.UpdatePlanet(name, type);
            return Ok("Update successfully");
        }

        [HttpDelete]
        [Route("DeletePlanet")]

        public JsonResult Delete(int planetId)
        {
            _planet.DeletePlanet(planetId);
            return new JsonResult("Deleted successfully");
        }

        [HttpGet]
        [Route("GetPlanetById/{Id}")]
  
        public async Task<IActionResult> GetPlanetById(int Id)
        {
            return Ok(await _planet.GetPlanetId(Id));
        }

        [HttpGet]
        [Route("GetPlanetByName")]

        public async Task<IActionResult> GetPlanetByName(string name)
        {
            return Ok(await _planet.GetPlanetByName(name));
        }







    }
}
