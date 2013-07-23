#region using

using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

#endregion

//TODO: Call search arameter URL on frontend
//TODO: Filter - Date sort order (from search obj)
//TODO: Sub-Grouping
//TODO: Export search to excel

namespace AptifyWebApi.Repository
{
    public class SearchRepository<T, TD> : NHibernateBaseRepository<ISession, T>
        where T : AptifriedMeeting
        where TD : AptifriedMeetingSearchDto
    {
        public SearchRepository(ISession session) : base(session) { }

        public IList<T> Search(TD sParams, bool useKeywordRanking)
        {

            //base: filter out any non-active items 
            var filterList = new List<Expression<Func<T, bool>>> ();

            Expression<Func<T, bool>> filterExpr = x => x.Status.Id == 1 && x.Product.WebEnabled && x.Product.IsSold;

            
            filterList.Add(filterExpr);

            if (sParams.IsDateSearch)
                filterList.Add(DateFilter(sParams));

            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < Context.GetActiveDbEducationCategoryCount())
                 filterList.Add(CreditTypeFilter(sParams));


            //TODO: Gut enum
            if (sParams.IsZipSearch && sParams.HasMeetingTypes)
            {
                var isIn =
                    sParams.MeetingTypes.Any(
                        x =>
                        x.Name.Equals(
                            EnumsAndConstantsToAvoidDatabaseChanges.MeetingTypeCategory.InPerson.Description()));

                if (isIn || sParams.MeetingTypes.Count() == Context.GetActiveDbMeetingTypesCount())
                    filterList.Add(ZipCodeFilter(sParams));

            }


            if (sParams.HasLevels)
                filterList.Add(LevelsFilter(sParams));

            //filterList.Add(MeetingTypesFilter(sParams));

            filterExpr = filterList.Aggregate(filterExpr, (current, expression) => current.AndCombine(expression));

            var qRes = Context.QueryOver<T>();
            var res = new List<T>();

            if (sParams.IsKeywordSearch)
                res =  qRes.Keyword(Context, sParams.SearchText, useKeywordRanking) as List<T>;

            res = new List<T>(res.Filter(filterExpr));

            //if results !ranked by keyword and !start/end date search, orderby date desc
            return !useKeywordRanking ? res.OrderByDescending(x => x.EndDate).ToList() : res;
        }


        #region Filters

        private Expression<Func<T, bool>> ZipCodeFilter(TD sParams)
        {
            var zips = Context.GetZipDistanceWeb(sParams.Zip,
                                                     sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            Expression<Func<T, bool>> expr = x => zips.Contains(x.Location.Id);

            return expr;
        }

        private Expression<Func<T, bool>> MeetingTypesFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr;

            //TODO: Use meeting type objects
            //TODO: Gut enum use
            if (sParams.HasMeetingTypes && sParams.MeetingType.Count < Context.GetActiveDbMeetingTypesCount())
            {
                var meetingTypeIds = new List<int>();

                foreach (var mType in sParams.MeetingType)
                    meetingTypeIds.AddRange(EnumHelper.GetMeetingTypeIdsByCategoryDescription(mType));

                expr = x => meetingTypeIds.Contains(x.Id);
            }
            else
                expr = x => x.Id != (int)EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.Session;

            return expr;
        }

        private static Expression<Func<T, bool>> LevelsFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr = x => sParams.Levels.Contains(x.ClassLevelId);

            return expr;
        }

        private static Expression<Func<T, bool>> CreditTypeFilter(TD sParams)
        {
            //grab selected ids
            var creditTypeIds = sParams.CreditTypes.Select(x => x.Id).Distinct();

            Expression<Func<T, bool>> expr = x => x.Credits.Any(y => creditTypeIds.Contains(y.Id));

            return expr;
        }

        private static Expression<Func<T, bool>> DateFilter(TD sParams)
        {
            //set to handle only start, only end or ranged date filter
            var sDate = sParams.StartDate.HasValue ? sParams.StartDate.Value : DateTime.Now;
            var eDate = sParams.EndDate.HasValue ? sParams.EndDate.Value : DateTime.MaxValue;

            //TODO: Gut enum
            //filter endDates that fall outside the specified range
            //exclude self study


            const int minDays = 7; // EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy.GetMinDays();
            
            //Using IsInRange extension method not supported
            //nor is GetLengthInDays
            Expression<Func<T, bool>> expr = x =>
                                                   (x.EndDate >= sDate && x.EndDate <= eDate) |
                                                   (x.StartDate - eDate).TotalDays >= minDays;
                               
            //x.Id == (int) EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy;
            return expr;
        }
        #endregion

        public T GetSingle(int id)
        {
            Expression<Func<T, bool>> expr = x => x.Id == id;

            return FindBy(expr).Single();
        }
    }

    //end class
}

//end namespace