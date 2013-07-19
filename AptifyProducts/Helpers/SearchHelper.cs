
 #region using

using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Expression = System.Linq.Expressions.Expression;

#endregion

namespace AptifyWebApi.Helpers
{
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public static class ExpressionUtility
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }

    public static class SearchHelper
    {
        private const string PrefixContains = "cn";
        private const string PrefixFreeText = "ft";

        public static bool IsInRange(this DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }

        public static double GetLengthInDays(this DateTime start, DateTime end)
        {
            var days = (end - start).Duration().TotalDays;

            return days;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate)
        {
            return queryable.Where(predicate);
        }

       
        public static IList<float> GetZipDistanceWeb(this ISession session, string startZip, string endZip)
        {
            //TODO: Refactor out sql
            var zips = session.CreateSQLQuery(
                String.Format(@"SELECT * FROM [dbo].[fnOSCPAGetZipDistanceWeb]({0},{1})",
                              startZip, endZip)).AddEntity(typeof (float)).List<float>();

            return zips;
        }

        public static int GetActiveDbMeetingTypesCount(this ISession session)
        {
            var r = session.GetActiveDbMeetingTypes().Count;

            return r;
        }

        public static IList<AptifriedMeetingType> GetActiveDbMeetingTypes(this ISession session)
        {
            var inUse =
                session.CreateSQLQuery(
                    "select distinct MeetingTypeId from Aptify.[dbo].vwmeetingstiny where MeetingTypeId is not null").List<int>();

            var qry = session.QueryOver<AptifriedMeetingType>().Where(x => x.Id.IsIn(inUse.ToArray()));

            return qry.List<AptifriedMeetingType>();

        }

        public static IList<AptifriedMeetingType> GetAllDbMeetingTypes(this ISession session)
        {
            var qry = session.QueryOver<AptifriedMeetingType>().List();

            return qry;
        }

        public static IList<AptifriedMeetingTypeDto> GetAllMeetingTypeDto(this ISession session)
        {
            var inUse = session.GetActiveDbMeetingTypes();

            return session.GetAllDbMeetingTypes().Select(x => new AptifriedMeetingTypeDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    Id = x.Id,
                    HasMeetings = inUse.Contains(x)
                }).ToList();
        }

        public static List<AptifriedEducationCategory> GetActiveDbEducationCategories(this ISession session)
        {
            var qry = session.Query<AptifriedEducationCategory>()
                             .Where(
                                 x =>
                                 x.Status.ToLowerInvariant()
                                  .Equals(
                                      EnumsAndConstantsToAvoidDatabaseChanges.EducationCategoryStatusActive
                                                                             .ToLowerInvariant()));

            return qry.ToList();
        }

        public static int GetActiveDbEducationCategoryCount(this ISession session)
        {
            var r = session.GetActiveDbEducationCategories().Count();

            return r;
        }


        public static IQueryable<T> Keyword<T>(this IQueryable<T> res, ISession session, string searchText,
                                               bool useKeywordRanking)
        {
            //WBN: Refactor out sql
            //Tell NH3 about freetext/contains sql functions and refactor to something like:
            //var res = res.Join(idsWeWant, x => x.Id, id => id, (x, id) => x);
            //res.Intersect(x => res.Keyword(sParams.SearchText));


            //No keyword search for you!
            if (String.IsNullOrWhiteSpace(searchText))
                return res;

            var searchBase = new StringBuilder();
            var searchWhere = new StringBuilder();
            var searchOrderBy = new StringBuilder();

            searchBase.AppendLine(
                @"SELECT mt.ID, mt.MeetingTitle, mt.StartDate, mt.EndDate, mt.OpenTime, mt.ClassLevelID, mt.ProductID, mt.StatusID, mt.MeetingTypeID, mt.AddressID, mt.VenueID");

            searchBase.AppendLine("from vwMeetingsTiny mt");
            searchBase.AppendLine("inner join vwStoreSearches s on s.ProductID = mt.ProductID");

            /**
           * Create the names of the tables, each with their own view of the rankings
           * based on the search input, and use some arbitrary mappings of how much
           * importance each should be judged to have.
           * 
           * After creating this mapping, create the derived rank calculation in the select above.
           * 
           * In the future, these should be derived evolutionarily or by some other ML method.
           **/

            IDictionary<string, uint> subrankMaps = new Dictionary<string, uint>
                {
                    {"ProductName", 25},
                    {"SearchableID", 25},
                    {"WhoShouldPurchase", 1},
                    {"Summary", 10},
                    {"Objective", 15},
                    {"AddtionalInformation", 1},
                    {"CreditTypes", 1},
                    {"Speakers", 25},
                    {"VenueName", 1},
                    {"VenueCity", 10},
                    {"Vendor", 3},
                    {"WebKeywords", 30},
                    {"MeetingType", 5},
                    {"Level", 0},
                };
            /**
                         * Create from a search string of "term1 term2 ... termn" the string
                         * "term1 near term2 ... near termn."
                         * 
                         * This could more flexibly be generated in the future by doing, e.g.,
                         * "term1 near term2 weight(x) ...," or other SQL methods.
                         * 
                         * http://msdn.microsoft.com/en-us/library/ms189760(v=SQL.90).aspx
                         **/
            var searchStringContainsTable = String.Format("'({0})'",
                                                          searchText.Split(' ')
                                                                    .Aggregate(String.Empty, (x, n) =>
                                                                                             x +
                                                                                             (!String.IsNullOrEmpty(x)
                                                                                                  ? " near "
                                                                                                  : String.Empty) + n));

            var searchStringFreeTextTables = String.Format("'{0}'", searchText);

            if (useKeywordRanking)
            {
                var rankString = String.Concat(GetRankString(subrankMaps, PrefixContains), " + ",
                                               GetRankString(subrankMaps, PrefixFreeText));

                // Filter out where relevance < epsilon
                const float epsilon = 0;
                searchWhere.AppendLine(String.Format("where {0} > {1}", rankString, epsilon));

                // We don't need to worry about clobbering any sort logic here because it won't have been defined yet
                searchOrderBy.AppendLine(String.Format("order by {0} desc, mt.startdate", rankString));
            }

            searchBase.BuildContainsTableJoins(searchStringContainsTable, subrankMaps);
            searchBase.BuildFreeTextTableJoins(searchStringFreeTextTables, subrankMaps);


            var qry = String.Concat(searchBase, searchWhere, searchOrderBy);

            var meetingQuery = session.CreateSQLQuery(qry)
                                      .AddEntity("mt", typeof (T));

            var meetings = meetingQuery.List<T>().AsQueryable();

            return meetings;
        }

        private static void BuildContainsTableJoins(this StringBuilder sb, string searchText,
                                                    IEnumerable<KeyValuePair<string, uint>> subrankMaps)
        {
            sb.BuildTableJoins(searchText, "CONTAINS", subrankMaps, PrefixContains);
        }

        private static void BuildFreeTextTableJoins(this StringBuilder sb, string searchText,
                                                    IEnumerable<KeyValuePair<string, uint>> subrankMaps)
        {
            sb.BuildTableJoins(searchText, "FREETEXT", subrankMaps, PrefixFreeText);
        }

        private static void BuildTableJoins(this StringBuilder sb, string searchText, string searchContainsOrFreeText,
                                            IEnumerable<KeyValuePair<string, uint>> subrankMaps, string prefix)
        {
            var rankEnumer = subrankMaps.GetEnumerator();

            while (rankEnumer.MoveNext())
            {
                sb.AppendLine(
                    String.Format("LEFT JOIN {0}TABLE(idxVwStoreSearch, {3}, {1} ) {2}{3} on {2}{3}.[KEY] = s.ID",
                                  searchContainsOrFreeText, searchText, prefix, rankEnumer.Current.Key));
            }
        }

        private static string GetRankString(IEnumerable<KeyValuePair<string, uint>> subrankMaps, string prefix)
        {
            var rankStatements = new LinkedList<string>();

            foreach (var pair in subrankMaps)
                rankStatements.AddLast(String.Format("{0} * ISNULL({1}{2}.rank,0)", pair.Value, prefix, pair.Key));

            var rankString = rankStatements.Aggregate(String.Empty,
                                                      (x, n) =>
                                                      x + (!String.IsNullOrEmpty(x) ? " + " : String.Empty) + n);

            return rankString;
        }
    }
}