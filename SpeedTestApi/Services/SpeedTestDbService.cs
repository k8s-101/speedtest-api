using System.Linq;
using System.Threading.Tasks;
using SpeedTestApi.Database;
using SpeedTestApi.Database.Models;
using SpeedTestApi.Dto;

namespace SpeedTestApi.Services
{
    public class SpeedTestDbService : ISpeedTestDbService
    {
        private readonly SpeedTestDbContext _database;

        public SpeedTestDbService(SpeedTestDbContext database)
        {
            _database = database;
        }

        public async Task AddTestResult(TestResult testResult)
        {
            var speedTest = SpeedTestResult.From(testResult);
            await _database.SpeedTestResults.AddAsync(speedTest);
            await _database.SaveChangesAsync();
        }

        public Task<TestResult[]> GetTestResults()
        {
            var testResults =
                _database.SpeedTestResults
                    .OrderByDescending(speedTest => speedTest.Timestamp)
                    .Take(10)
                    .Select(speedTest => speedTest.ToTestResult())
                    .ToArray();

            return Task.FromResult(testResults);
        }
    }
}