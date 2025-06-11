using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IEGEasyCreditcardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private static readonly Random _random = new();
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpGet("status")]
        public IActionResult TriggerError()
        {
            if (_random.Next(1, 3) == 1)
            {
                var request = this.Request;
                _logger.LogError($"{request.Host}: Simulated failure.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Simulated failure.");
            }

            return Ok("IEGEasyCreditcardService is running");

        }
    }
}
