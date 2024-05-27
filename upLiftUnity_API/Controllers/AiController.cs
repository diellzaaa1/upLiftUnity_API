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
                // Return a specific error message
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "AI nuk eshte i qasshem per momentin");
            }
        }
    }
}
