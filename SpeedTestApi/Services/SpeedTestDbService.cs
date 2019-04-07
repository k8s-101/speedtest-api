using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeedTestApi.Database;
using SpeedTestApi.Database.Models;
using SpeedTestApi.Dto;
using SpeedTestApi.Query;

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

        public async Task<TestResult[]> GetTestResults(QueryParameters parameters)
        {
            return await
                _database.SpeedTestResults
                    .FilterTickets(parameters)
                    .SortTickets(parameters)
                    .PaginateTickets(parameters)
                    .Select(speedTest => speedTest.ToTestResult())
                    .ToArrayAsync();
        }
    }
}