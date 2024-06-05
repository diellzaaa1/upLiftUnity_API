using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories
{
    public interface IFeedbackRepository
    {
        Task<Feedback> InsertFeedback(Feedback feedback);

        Task<IEnumerable<Feedback>> GetFeedback();

        Task<Feedback> GetFeedbackById(int id);

        Task<Feedback> UpdateFeedback(Feedback feedback);

        Task DeleteFeedback(int id);
    }
}