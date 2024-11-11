using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.MembersRepository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly APIDbContext _dbContext;

        public MemberRepository(APIDbContext dbContext)
        {
               _dbContext = dbContext;
        }
        public async Task CreateMember(string name, string role, int groupId)
        {
            var member = new Member {
                Name = name, 
                Role = role, 
                GroupId = groupId };

            await _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMember(int memberId)
        {
            var memberInDb = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == memberId);
            if(memberInDb == null)
            {
                throw new InvalidOperationException("Group was not found!");
            }
            _dbContext.Members.Remove(memberInDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _dbContext.Members
                .Include(x=>x.Group)
                .ToListAsync();
        }
    }
}
