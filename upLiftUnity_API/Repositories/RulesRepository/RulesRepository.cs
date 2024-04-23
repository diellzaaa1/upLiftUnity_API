using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories
{
    public class RulesRepository : IRulesRepository
    {
        private readonly APIDbContext _dbContext;

        public RulesRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Rules>> GetRules()
        {
            return await _dbContext.Rules.ToListAsync();
        }

        public async Task<Rules> GetRuleById(int id)
        {
            return await _dbContext.Rules.FindAsync(id);
        }

        public async Task<Rules> InsertRule(Rules rule)
        {
            _dbContext.Rules.Add(rule);
            await _dbContext.SaveChangesAsync();
            return rule;
        }

        public async Task<Rules> UpdateRule(Rules rule)
        {
            _dbContext.Entry(rule).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return rule;
        }

        public async Task DeleteRule(int id)
        {
            var rule = await _dbContext.Rules.FindAsync(id);
            if (rule != null)
            {
                _dbContext.Rules.Remove(rule);
                await _dbContext.SaveChangesAsync();
            }
        }

      
    }
}
