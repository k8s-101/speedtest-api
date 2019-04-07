using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeedTestApi.Dto;
using SpeedTestApi.Query;
using SpeedTestApi.Services;

namespace SpeedTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpeedTestController : ControllerBase
    {
        private readonly ISpeedTestDbService _dbService;

        public SpeedTestController(ISpeedTestDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("ping")]
        public string Ping()
        {
            Console.WriteLine("GET /SpeedTest/ping");

            return "PONG";
        }

        // GET /SpeedTest
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<TestResult[]> GetSpeedTests([FromQuery] QueryParameters parameters)
        {
            Console.WriteLine("GET /SpeedTest");

            var speedTests = await _dbService.GetTestResults(parameters);

            return speedTests;
        }

        // POST /SpeedTest
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadSpeedTest([FromBody] TestResult speedTest)
        {
            Console.WriteLine("POST /SpeedTest with SessionId: {0}", speedTest.SessionId);

            await _dbService.AddTestResult(speedTest);

            return Ok();
        }
    }
}