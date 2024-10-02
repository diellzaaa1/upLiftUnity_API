using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.BuildingDtos;
using upLiftUnity_API.DTOs.PlanetsDto;
using upLiftUnity_API.Repositories.BuildinggRepository;
using upLiftUnity_API.Repositories.PlanetsRepository;

namespace upLiftUnity_API.Controllers
{
    public class BuildingController : ControllerBase
    {
        private readonly IBuildinggRepository _buildinggRepository;
        public BuildingController(IBuildinggRepository buildinggRepository)
        {
            _buildinggRepository = buildinggRepository;
        }


        [HttpPost]
        public async Task<IActionResult> AddBuilding([FromBody] BuildingDto buildingDto)
        {
            await _buildinggRepository.AddBuilding(buildingDto);

            return Ok(buildingDto);
        }

        [HttpPost("renovations")]
        public async Task<IActionResult> AddRenovation([FromBody] RenovationDto renovationDto)
        {
            await _buildinggRepository.AddRenovation(renovationDto);

            return Ok(renovationDto);
        }

        [HttpGet("/buildings")]
        public async Task<IActionResult> GetBuildinggs()
        {
            return Ok(await _buildinggRepository.GetBuildinggs());
        }

        [HttpGet("/renovations")]
        public async Task<IActionResult> GetRenovationns()
        {
            return Ok(await _buildinggRepository.GetRenovationns());
        }

        [HttpDelete("renovations/{id:int}")]
        public async Task<IActionResult> DeleteRenovation(int renovationId)
        {
            await _buildinggRepository.DeleteRenovation(renovationId);
            return Ok();
        }
    }
}
