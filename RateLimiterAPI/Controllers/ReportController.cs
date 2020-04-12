using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RateLimiter.Business.BL.Interfaces;
using RateLimiterAPI.Requests;

namespace RateLimiterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;

        private readonly IRateLimiter _limiter;

        public ReportController(ILogger<ReportController> logger, IRateLimiter limiter)
        {
            _logger = logger;
            _limiter = limiter;
        }



        [HttpPost]
        public async Task<ActionResult> Report(URLRequest request)
        {
            var url = request.URL;
            if (url == null)
            {
                return BadRequest("URL is missing");
            }

            _logger.LogInformation($"Got request for url: {url}");

            var limitResult = await _limiter.GetLimitStatusByIdentifier(url);
            return Ok(limitResult);
        }
    }
}
