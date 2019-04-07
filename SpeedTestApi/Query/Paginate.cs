using System.Linq;
using SpeedTestApi.Database.Models;

namespace SpeedTestApi.Query
{
    public static class Paginate
    {
        public static IQueryable<SpeedTestResult> PaginateTickets(this IQueryable<SpeedTestResult> query, QueryParameters param)
        {
            var p = Pagination(param.From, param.To);

            return query.Skip(p.startIndex).Take(p.noOfSpeedTests);
        }

        private static (int startIndex, int noOfSpeedTests) Pagination(int? from, int? to)
        {
            if (!from.HasValue && to.HasValue && to.Value >= 0)
            {
                return (0, to.Value);
            }

            var defaultPagination = (0, 25);
            if (!from.HasValue || !to.HasValue)
            {
                return defaultPagination;
            }

            if (from.Value < 0 || to.Value < 0)
            {
                return defaultPagination;
            }

            if (from.Value >= to.Value)
            {
                return defaultPagination;
            }

            var noOfSpeedTests = to.Value - from.Value;

            return (startIndex: from.Value, noOfSpeedTests: noOfSpeedTests);
        }
    }
}