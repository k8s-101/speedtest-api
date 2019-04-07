using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SpeedTestApi.Dto;

namespace SpeedTestApi.Database.Models
{
    public class SpeedTestResult
    {
        [Key]
        public int SpeedTestResultId { get; set; }

        [Required]
        [MaxLength(36)]
        public string SessionId { get; set; }

        [Required]
        [MaxLength(500)]
        public string User { get; set; }

        [Required]
        public int Device { get; set; }

        [Required]
        public long Timestamp { get; set; }

        [Required]
        public double DownloadSpeed { get; set; }

        [Required]
        public double UploadSpeed { get; set; }

        [Required]
        public string ClientIp { get; set; }

        [Required]
        public double ClientLatitude { get; set; }

        [Required]
        public double ClientLongitude { get; set; }

        [Required]
        [MaxLength(500)]
        public string ClientIsp { get; set; }

        [Required]
        [MaxLength(500)]
        public string ClientCountry { get; set; }

        [Required]
        [MaxLength(500)]
        public string ServerHost { get; set; }

        [Required]
        public double ServerLatitude { get; set; }

        [Required]
        public double ServerLongitude { get; set; }

        [Required]
        [MaxLength(500)]
        public string ServerCountry { get; set; }

        [Required]
        public double ServerDistanceToClient { get; set; }

        [Required]
        public int ServerPing { get; set; }

        [Required]
        public int ServerId { get; set; }

        public static SpeedTestResult From(TestResult testResult)
        {
            var speeds = testResult.Data.Speeds;
            var client = testResult.Data.Client;
            var server = testResult.Data.Server;

            return new SpeedTestResult
            {
                SessionId = testResult.SessionId.ToString(),
                User = testResult.User,
                Device = testResult.Device,
                Timestamp = testResult.Timestamp,
                DownloadSpeed = speeds.Download,
                UploadSpeed = speeds.Upload,
                ClientIp = client.Ip,
                ClientLatitude = client.Latitude,
                ClientLongitude = client.Longitude,
                ClientIsp = client.Isp,
                ClientCountry = client.Country,
                ServerHost = server.Host,
                ServerLatitude = server.Latitude,
                ServerLongitude = server.Longitude,
                ServerCountry = server.Country,
                ServerDistanceToClient = server.Distance,
                ServerPing = server.Ping,
                ServerId = server.Id,
            };
        }

        public TestResult ToTestResult()
        {
            return new TestResult
            {
                SessionId = Guid.Parse(SessionId),
                User = User,
                Device = Device,
                Timestamp = Timestamp,
                Data = new TestData
                {
                    Speeds = new TestSpeeds
                    {
                        Download = DownloadSpeed,
                        Upload = UploadSpeed,
                    },
                    Client = new TestClient
                    {
                        Ip = ClientIp,
                        Latitude = ClientLatitude,
                        Longitude = ClientLongitude,
                        Isp = ClientIsp,
                        Country = ClientCountry,
                    },
                    Server = new TestServer
                    {
                        Host = ServerHost,
                        Latitude = ServerLatitude,
                        Longitude = ServerLongitude,
                        Country = ServerCountry,
                        Distance = ServerDistanceToClient,
                        Ping = ServerPing,
                        Id = ServerId,
                    }
                }
            };
        }
    }
}