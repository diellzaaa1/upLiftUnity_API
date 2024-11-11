using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.MembersRepository
{
    public interface IMemberRepository
    {

        Task<List<Member>> GetAllMembers();
        Task CreateMember(string name, string role, int groupId);
        Task DeleteMember(int memberId);

    }
}
