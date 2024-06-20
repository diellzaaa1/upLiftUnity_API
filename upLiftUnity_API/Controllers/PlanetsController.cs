using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.PlanetsDto;
using upLiftUnity_API.Repositories.PlanetsRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/planets")]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetRepository _planetsRepository;
        public PlanetsController(IPlanetRepository planetRepository)
        {
            _planetsRepository = planetRepository;
        }


        [HttpPost]
        public async Task<IActionResult> AddPlanet([FromBody] PlanetDto planetDto)
        {
            await _planetsRepository.AddPlanet(planetDto);

            return Ok(planetDto);
        }

        [HttpGet("{name}/satelites")]
        public async Task<IActionResult> GetPlanetSatelies(string name)
        {
            return Ok(await _planetsRepository.GetPlanetSatelites(name));
        }
        [HttpGet("/planets")]
        public async Task<IActionResult> GetPlanets()
        {
            return Ok(await _planetsRepository.GetPlanets());
        }
        [HttpGet("/satelites")]
        public async Task<IActionResult> GetSatelites()
        {
            return Ok(await _planetsRepository.GetSatelites());
        }

        [HttpPost("satelites")]
        public async Task<IActionResult> AddPlanetSatelite([FromBody] SateliteDto sateliteDto)
        {
            await _planetsRepository.AddSatelite(sateliteDto);
            return Ok(sateliteDto);
        }

        [HttpPut("{name}/type")]
        public async Task<IActionResult> UpdatePlanetType(string name, [FromBody] UpdatePlanetTypeDto updatePlanetTypeDto)
        {
            await _planetsRepository.UpdatePlanet(name, updatePlanetTypeDto.NewType);

            return Ok(updatePlanetTypeDto);
        }

        [HttpDelete("satelites/{id:int}")]
        public async Task<IActionResult> DeleteSatelite(int id)
        {
            await _planetsRepository.DeleteSatelite(id);
            return Ok();
        }
    }
}
