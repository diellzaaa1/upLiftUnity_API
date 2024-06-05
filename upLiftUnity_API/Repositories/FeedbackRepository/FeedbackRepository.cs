using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly APIDbContext _dbContext;

        public FeedbackRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Feedback>> GetFeedback()
        {
            return await _dbContext.Feedback.ToListAsync();
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            return await _dbContext.Feedback.FindAsync(id);
        }

        public async Task<Feedback> InsertFeedback(Feedback feedback)
        {
            _dbContext.Feedback.Add(feedback);
            await _dbContext.SaveChangesAsync();
            return feedback;
        }

        public async Task<Feedback> UpdateFeedback(Feedback feedback)
        {
            _dbContext.Entry(feedback).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return feedback;
        }

        public async Task DeleteFeedback(int id)
        {
            var feedback = await _dbContext.Feedback.FindAsync(id);
            if (feedback != null)
            {
                _dbContext.Feedback.Remove(feedback);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
