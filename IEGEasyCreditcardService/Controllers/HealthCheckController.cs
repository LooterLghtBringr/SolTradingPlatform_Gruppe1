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
            if (_random.Next(1, 5) == 1)
            {
                _logger.LogError("Simulated failure.");
                throw new Exception("Random failure occurred.");
            }

            return Ok("IEGEasyCreditcardService is running");

        }
    }
}
