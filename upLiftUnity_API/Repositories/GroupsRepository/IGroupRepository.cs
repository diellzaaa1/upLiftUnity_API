using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.GroupsRepository
{
    public interface IGroupRepository
    {

        Task CreateGroup(string groupName, string description);
        Task DeleteGroup(int id);
        Task UpdateGroup(int id, string groupName, string description);
        Task<List<Group>> GetAllGroups();
        Task<Group> GetGroupById(int id);
    }
}
