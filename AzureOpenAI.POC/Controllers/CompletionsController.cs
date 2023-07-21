using AzureOpenAI.POC.Models;
using AzureOpenAI.POC.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureOpenAI.POC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetCompletions(UserMessage userMessage, [FromServices] OpenAICompletionsService openAICompletionsService)
        {
            string response = await openAICompletionsService.Handle(userMessage.Content);

            return Ok(response);
        }
    }
}
