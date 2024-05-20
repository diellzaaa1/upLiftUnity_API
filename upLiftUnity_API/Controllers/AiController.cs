using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAIBasedResult(string searchText)
        {
            // Set your API key here
            string APIKey = "sk-proj-1gWiHtoS6sjsMuQs4beGT3BlbkFJWRnkROa1dBZPHYTY9v7J";

            try
            {
                // Initialize OpenAI API with your API key
                var openai = new OpenAIAPI(APIKey);

                // Create a CompletionRequest object
                var completionRequest = new CompletionRequest
                {
                    Prompt = searchText,
                    Model = OpenAI_API.Models.Model.DavinciText,
                    MaxTokens = 1024 // Adjust as needed
                };

                // Get AI completion result
                var result = await openai.Completions.CreateCompletionAsync(completionRequest);

                // Concatenate completion texts
                string answer = "";
                foreach (var item in result.Completions)
                {
                    answer += item.Text;
                }

                // Return the completion result
                return Ok(answer);
            }
            catch (Exception ex)
            {
                // Check if the exception is due to quota exceeded
                if (ex.Message.Contains("TooManyRequests"))
                {
                    // Return a specific error message for quota exceeded
                    return StatusCode(StatusCodes.Status429TooManyRequests, "API quota exceeded. Please try again later.");
                }
                else
                {
                    // Handle other exceptions and return an error response
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                }
            }
        }
    }
}



