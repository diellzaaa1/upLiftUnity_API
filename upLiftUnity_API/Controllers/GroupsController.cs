using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.GroupsDto;
using upLiftUnity_API.Repositories.GroupsRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var result =  await _groupRepository.GetAllGroups();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupDto dto)
        {
            await _groupRepository.CreateGroup(dto.GroupName, dto.Description);
            return Ok("Created succesfully!");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupRepository.DeleteGroup(id);
            return Ok("Group deleted!");
        }

        [HttpPut("id")]

        public async Task<IActionResult> UpdateGroup(int id,[FromBody] UpdateGroupDto dto)
        {
            await _groupRepository.UpdateGroup(id,dto.GroupName,dto.Description);
            return Ok("Updated successfully!");
        }
    }
}
