using Microsoft.AspNetCore.Mvc;

namespace ValueService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly string _baseUrl;
        private readonly HttpContext _context;

        public ValuesController(ILogger<ValuesController> logger, IHttpContextAccessor context)
        {
            if (context.HttpContext != null)
            {
                _context = context.HttpContext;
                var request = _context.Request;
                _baseUrl = $"{request.Scheme}://{request.Host}";
            }
            _logger = logger;
        }

        [HttpGet("badcode")]
        public string BadCode()
        {
            throw new Exception("Some bad code was executed!");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ocelotReqId = _context.Request.Headers["OcRequestId"];
            if (string.IsNullOrEmpty(ocelotReqId))
            {
                ocelotReqId = "N/A";
            }
            var result = $" Alekta Movit Movit, Url: {_baseUrl}, Method: {_context.Request.Method}, Path: {_context.Request.Path}, OcelotRequestId: {ocelotReqId}";
            _logger.LogInformation(result);
            return Ok(result);
        }

        [HttpGet("healthcheck")]
        public IActionResult Healthcheck()
        {
            var msg = $"{_context.Request.Host} is healthy";
            _logger.LogInformation(msg);
            return Ok(msg);
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            var msg = $"Running on {_context.Request.Host} Alekta Movit Movit";
            _logger.LogInformation(msg);
            return Ok(msg);
        }
    }

}
