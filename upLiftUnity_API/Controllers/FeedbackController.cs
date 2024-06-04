using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories;



namespace upLiftUnity_API.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository ?? throw new ArgumentNullException(nameof(feedbackRepository));
        }

        [HttpGet]
        [Route("GetFeedback")]

        public async Task<IActionResult> Get()
        {
            var feedback = await _feedbackRepository.GetFeedback();
            return Ok(feedback);
        }

        [HttpGet]
        [Route("GetFeedbackById/{id}")]

        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedbackRepository.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        [HttpPost]
        [Route("AddFeedback")]

        public async Task<IActionResult> AddFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _feedbackRepository.InsertFeedback(feedback);
            if (result == null || result.FeedbackId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add the feedback");
            }
            return Ok("Feedback added successfully");
        }

        [HttpPut]
        [Route("UpdateFeedback/{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedFeedback = await _feedbackRepository.UpdateFeedback(feedback);

            if (updatedFeedback == null)
            {
                return NotFound();
            }

            return Ok("Feedback updated successfully");
        }

        [HttpDelete]
        [Route("DeleteFeedback/{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedbackToDelete = await _feedbackRepository.GetFeedbackById(id);
            if (feedbackToDelete == null)
            {
                return NotFound();
            }

            await _feedbackRepository.DeleteFeedback(id);
            return Ok("Feedback deleted successfully");
        }
    }
}