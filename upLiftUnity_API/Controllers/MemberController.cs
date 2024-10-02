using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.MemberDto;
using upLiftUnity_API.Repositories.MembersRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var result = await _memberRepository.GetAllMembers();
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> CreateMember([FromBody] CreateMemberDto dto)
        {
            await _memberRepository.CreateMember(dto.Name, dto.Role, dto.GroupId);
            return Ok("Created succsessfully!");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _memberRepository.DeleteMember(id);
            return Ok("Deleted Member success!");
        }
    }
}
