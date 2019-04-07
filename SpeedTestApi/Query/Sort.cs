using System;
using System.Linq;
using System.Linq.Expressions;
using SpeedTestApi.Database.Models;

namespace SpeedTestApi.Query
{
    public enum SortableColumn
    {
        SessionId,
        User,
        Device,
        TestDate,
        DownloadSpeed,
        UploadSpeed,
        ClientIp,
        ClientLatitude,
        ClientLongitude,
        ClientIsp,
        ClientCountry,
        ServerHost,
        ServerLatitude,
        ServerLongitude,
        ServerCountry,
        ServerDistanceToClient,
        ServerPing,
        ServerId,
    }

    public enum Ordering
    {
        Asc,
        Desc,
    }

    public static class Sort
    {
        public static IQueryable<SpeedTestResult> SortTickets(this IQueryable<SpeedTestResult> query, QueryParameters param)
        {
            if (param.HasNoSort())
            {
                return query.OrderByDescending(speedTest => speedTest.TestDate);
            }

            switch (param.SortOn.Value)
            {
                case SortableColumn.SessionId:
                    return query.SortBy(speedTest => speedTest.SessionId, param.SortOrder.Value);
                case SortableColumn.User:
                    return query.SortBy(speedTest => speedTest.User, param.SortOrder.Value);
                case SortableColumn.Device:
                    return query.SortBy(speedTest => speedTest.Device, param.SortOrder.Value);
                case SortableColumn.TestDate:
                    return query.SortBy(speedTest => speedTest.TestDate, param.SortOrder.Value);
                case SortableColumn.DownloadSpeed:
                    return query.SortBy(speedTest => speedTest.DownloadSpeed, param.SortOrder.Value);
                case SortableColumn.UploadSpeed:
                    return query.SortBy(speedTest => speedTest.UploadSpeed, param.SortOrder.Value);
                case SortableColumn.ClientIp:
                    return query.SortBy(speedTest => speedTest.ClientIp, param.SortOrder.Value);
                case SortableColumn.ClientLatitude:
                    return query.SortBy(speedTest => speedTest.ClientLatitude, param.SortOrder.Value);
                case SortableColumn.ClientLongitude:
                    return query.SortBy(speedTest => speedTest.ClientLongitude, param.SortOrder.Value);
                case SortableColumn.ClientIsp:
                    return query.SortBy(speedTest => speedTest.ClientIsp, param.SortOrder.Value);
                case SortableColumn.ClientCountry:
                    return query.SortBy(speedTest => speedTest.ClientCountry, param.SortOrder.Value);
                case SortableColumn.ServerHost:
                    return query.SortBy(speedTest => speedTest.ServerHost, param.SortOrder.Value);
                case SortableColumn.ServerLatitude:
                    return query.SortBy(speedTest => speedTest.ServerLatitude, param.SortOrder.Value);
                case SortableColumn.ServerLongitude:
                    return query.SortBy(speedTest => speedTest.ServerLongitude, param.SortOrder.Value);
                case SortableColumn.ServerCountry:
                    return query.SortBy(speedTest => speedTest.ServerCountry, param.SortOrder.Value);
                case SortableColumn.ServerDistanceToClient:
                    return query.SortBy(speedTest => speedTest.ServerDistanceToClient, param.SortOrder.Value);
                case SortableColumn.ServerPing:
                    return query.SortBy(speedTest => speedTest.ServerPing, param.SortOrder.Value);
                case SortableColumn.ServerId:
                    return query.SortBy(speedTest => speedTest.ServerId, param.SortOrder.Value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static bool HasNoSort(this QueryParameters param)
        {
            return !param.SortOn.HasValue || !param.SortOrder.HasValue;
        }

        private static IOrderedQueryable<SpeedTestResult> SortBy<T>(this IQueryable<SpeedTestResult> query, Expression<Func<SpeedTestResult, T>> exp, Ordering order)
        {
            return order == Ordering.Desc ? query.OrderByDescending(exp) : query.OrderBy(exp);
        }
    }
}