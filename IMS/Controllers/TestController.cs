using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
            _logger.LogInformation("TestController Started");
        }
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Executing TestController Get Method");
            return Ok();
        }
    }
}
