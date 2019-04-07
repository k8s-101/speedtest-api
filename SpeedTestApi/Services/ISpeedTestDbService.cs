using System.Threading.Tasks;
using SpeedTestApi.Dto;

namespace SpeedTestApi.Services
{
    public interface ISpeedTestDbService
    {
        Task<TestResult[]> GetTestResults();

        Task AddTestResult(TestResult dtoResult);
    }
}