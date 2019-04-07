using System;
using SpeedTestApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SpeedTestApi.Query
{
    public class QueryParameters
    {
        [FromQuery]
        public string SessionId { get; set; }

        [FromQuery]
        public string User { get; set; }

        [FromQuery]
        public int? Device { get; set; }

        [FromQuery]
        public DateTime? TestDateGt { get; set; }

        [FromQuery]
        public DateTime? TestDateLt { get; set; }

        [FromQuery]
        public double? DownloadSpeedGt { get; set; }

        [FromQuery]
        public double? DownloadSpeedLt { get; set; }

        [FromQuery]
        public double? UploadSpeedGt { get; set; }

        [FromQuery]
        public double? UploadSpeedLt { get; set; }

        [FromQuery]
        public string ClientIp { get; set; }

        [FromQuery]
        public double? ClientLatitude { get; set; }

        [FromQuery]
        public double? ClientLongitude { get; set; }

        [FromQuery]
        public string ClientIsp { get; set; }

        [FromQuery]
        public string ClientCountry { get; set; }

        [FromQuery]
        public string ServerHost { get; set; }

        [FromQuery]
        public double? ServerLatitude { get; set; }

        [FromQuery]
        public double? ServerLongitude { get; set; }

        [FromQuery]
        public string ServerCountry { get; set; }

        [FromQuery]
        public double? ServerDistanceToClient { get; set; }

        [FromQuery]
        public int? ServerPing { get; set; }

        [FromQuery]
        public int? ServerId { get; set; }

        [FromQuery]
        public SortableColumn? SortOn { get; set; }

        [FromQuery]
        public Ordering? SortOrder { get; set; }

        [FromQuery]
        public int? From { get; set; }

        [FromQuery]
        public int? To { get; set; }
    }
}