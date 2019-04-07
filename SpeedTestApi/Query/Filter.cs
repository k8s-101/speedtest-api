using System;
using System.Linq;
using System.Linq.Expressions;
using SpeedTestApi.Database.Models;

namespace SpeedTestApi.Query
{
    public static class Filter
    {
        public static IQueryable<SpeedTestResult> FilterTickets(this IQueryable<SpeedTestResult> query, QueryParameters param)
        {
            return query
                .FilterParam(param.SessionId, speedTest => speedTest.SessionId == param.SessionId)
                .FilterParam(param.User, speedTest => speedTest.User == param.User)
                .FilterParam(param.Device, speedTest => speedTest.Device == param.Device)
                .FilterParam(param.TestDateGt, speedTest => speedTest.TestDate >= param.TestDateGt.Value.StartOfDay())
                .FilterParam(param.TestDateLt, speedTest => speedTest.TestDate <= param.TestDateLt.Value.EndOfDay())
                .FilterParam(param.DownloadSpeedGt, speedTest => speedTest.DownloadSpeed >= param.DownloadSpeedGt)
                .FilterParam(param.DownloadSpeedLt, speedTest => speedTest.DownloadSpeed <= param.DownloadSpeedLt)
                .FilterParam(param.UploadSpeedGt, speedTest => speedTest.UploadSpeed >= param.UploadSpeedGt)
                .FilterParam(param.UploadSpeedLt, speedTest => speedTest.UploadSpeed <= param.UploadSpeedLt)
                .FilterParam(param.ClientIp, speedTest => speedTest.ClientIp == param.ClientIp)
                .FilterParam(param.ClientLatitude, speedTest => speedTest.ClientLatitude == param.ClientLatitude)
                .FilterParam(param.ClientLongitude, speedTest => speedTest.ClientLongitude == param.ClientLongitude)
                .FilterParam(param.ClientIsp, speedTest => speedTest.ClientIsp == param.ClientIsp)
                .FilterParam(param.ClientCountry, speedTest => speedTest.ClientCountry == param.ClientCountry)
                .FilterParam(param.ServerHost, speedTest => speedTest.ServerHost == param.ServerHost)
                .FilterParam(param.ServerLatitude, speedTest => speedTest.ServerLatitude == param.ServerLatitude)
                .FilterParam(param.ServerLongitude, speedTest => speedTest.ServerLongitude == param.ServerLongitude)
                .FilterParam(param.ServerCountry, speedTest => speedTest.ServerCountry == param.ServerCountry)
                .FilterParam(param.ServerDistanceToClient, speedTest => speedTest.ServerDistanceToClient == param.ServerDistanceToClient)
                .FilterParam(param.ServerPing, speedTest => speedTest.ServerPing == param.ServerPing)
                .FilterParam(param.ServerId, speedTest => speedTest.ServerId == param.ServerId);
        }

        private static IQueryable<SpeedTestResult> FilterParam(this IQueryable<SpeedTestResult> query, string param, Expression<Func<SpeedTestResult, bool>> exp)
        {
            return string.IsNullOrEmpty(param) ? query : query.Where(exp);
        }

        private static IQueryable<SpeedTestResult> FilterParam<T>(this IQueryable<SpeedTestResult> query, T? param, Expression<Func<SpeedTestResult, bool>> exp)
        where T : struct
        {
            return param.HasValue ? query.Where(exp) : query;
        }

        private static DateTime StartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        private static DateTime EndOfDay(this DateTime dateTime)
        {
            return StartOfDay(dateTime).AddDays(1).AddTicks(-1);
        }
    }
}