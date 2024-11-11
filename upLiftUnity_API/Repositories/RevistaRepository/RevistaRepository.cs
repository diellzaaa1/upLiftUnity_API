using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.RevistaRepository
{
    public class RevistaRepository : IRevistaRepository
    {
        private readonly APIDbContext _dbContext;

        public RevistaRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateRevista(string magazineName, int issueNumber, int botuesiId)
        {
            var revista = new Revista
            {
                MagazineName = magazineName,
                IssueNumber = issueNumber,
                PublisherId = botuesiId
            };

            await _dbContext.Revistat.AddAsync(revista);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Revista>> GetAllRevistat()
        {
            return await _dbContext.Revistat
                .ToListAsync();
        }
    }
}
