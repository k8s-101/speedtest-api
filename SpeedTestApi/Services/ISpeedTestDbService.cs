using System.Threading.Tasks;
using SpeedTestApi.Dto;
using SpeedTestApi.Query;

namespace SpeedTestApi.Services
{
    public interface ISpeedTestDbService
    {
        Task<TestResult[]> GetTestResults(QueryParameters parameters);

        Task AddTestResult(TestResult dtoResult);
    }
}