using Microsoft.AspNetCore.Mvc;
using RCGTestBackend.Domain.Interfaces.Services;

namespace RCGTestBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringProcessingController : ControllerBase
    {
        IStringProcessingService StringProcessingService;

        public StringProcessingController(IStringProcessingService stringProcessingService)
        {
            StringProcessingService = stringProcessingService;
        }

        [HttpGet]
        [Route("GetBase64EncodedString/{plainText}")]
        public async Task<IActionResult> GetBase64EncodedString(CancellationToken cancellationToken, [FromRoute]string plainText)
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            var encodedString = StringProcessingService.EncodeToBase64(plainText);

            try
            {
                await Response.WriteAsync($"data: </START {encodedString.Length}/>\n\n");

                for (var i = 0; i < encodedString.Length; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return Ok();
                    }

                    await Response.WriteAsync($"data: {encodedString[i]}\n\n");

                    var random = new Random();
                    var interval = random.Next(1000, 5000);

                    await Task.Delay(interval);
                }

                await Response.WriteAsync($"data: </END/>\n\n");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

