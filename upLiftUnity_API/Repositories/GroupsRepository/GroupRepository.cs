
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.GroupsRepository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly APIDbContext _dbContext;
        public GroupRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateGroup(string groupName, string description)
        {
            var group = new Group()
            {
                GroupName = groupName,
                Description = description
            };

            await _dbContext.Groups.AddAsync(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGroup(int id)
        {
            var groupInDb = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (groupInDb == null)
            {
                throw new InvalidOperationException("Group was not found!");
            }

            _dbContext.Groups.Remove(groupInDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _dbContext.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateGroup(int id, string groupName, string description)
        {
            var groupInDb = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (groupInDb == null)
            {
                throw new InvalidOperationException("Group was not found!");
            }

            groupInDb.GroupName = groupName;
            groupInDb.Description = description;

            await _dbContext.SaveChangesAsync();
        }
    }
}
