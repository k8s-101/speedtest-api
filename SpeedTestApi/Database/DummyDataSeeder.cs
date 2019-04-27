using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpeedTestApi.Database.Models;

namespace SpeedTestApi.Database
{
    public static class DummyDataSeeder
    {
        public static IApplicationBuilder UseDummyDataSeed(this IApplicationBuilder application)
        {
            var services = application.ApplicationServices;
            var dummySpeedTests =
                QuarterHoursLastFourteenDays()
                .Select(date => DummySpeedTestResult(date));

            using (var scope = services.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<SpeedTestDbContext>())
            {
                context.Database.EnsureCreated();
                context.SpeedTestResults.AddRange(dummySpeedTests);
                context.SaveChanges();
            }

            return application;
        }

        private static IEnumerable<DateTime> QuarterHoursLastFourteenDays()
        {
            var now = DateTime.Now;
            var numberOfMinutesInFourteenDays = 14 * 24 * 60;
            for (int dt = 0; dt <= numberOfMinutesInFourteenDays; dt = dt + 15)
            {
                yield return now.AddMinutes(-dt);
            }
        }

        private static SpeedTestResult DummySpeedTestResult(DateTime testDate)
        {
            var random = new Random();
            return new SpeedTestResult
            {
                SessionId = Guid.NewGuid().ToString(),
                User = "teodoran",
                Device = 3,
                TestDate = testDate,
                DownloadSpeed = random.Next(60, 100),
                UploadSpeed = random.Next(5, 25),
                ClientIp = "127.0.0.1",
                ClientLatitude = 59.913396,
                ClientLongitude = 10.741330,
                ClientIsp = "UUNET",
                ClientCountry = "Norway",
                ServerHost = "Norsk Data",
                ServerLatitude = 59.902138,
                ServerLongitude = 10.771267,
                ServerCountry = "Norway",
                ServerDistanceToClient = 2430,
                ServerPing = random.Next(6, 20),
                ServerId = 42,
            };
        }
    }
}
