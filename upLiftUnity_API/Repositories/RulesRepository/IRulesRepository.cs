using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories
{
    public interface IRulesRepository
    {
        Task<Rules> InsertRule(Rules rule);

        Task<IEnumerable<Rules>> GetRules();

        Task<Rules> GetRuleById(int id);

        Task<Rules> UpdateRule(Rules rule);

        Task DeleteRule(int id);
    }
}
