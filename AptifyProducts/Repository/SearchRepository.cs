
#region using

using System.Collections;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;

#endregion

//TODO: Sub-Grouping
//TODO: Export search to excel
//TODO: Initial search: Default to distance based on profile info, order by date

//IN: TEST: Call search parameter URL on frontend

//TODO: Filter - Date sort order (from search obj)


namespace AptifyWebApi.Repository
{
    public class SearchRepository<T, TD> : NHibernateBaseRepository<ISession, T>
        where T : AptifriedMeeting
        where TD : AptifriedMeetingSearchDto
    {
        public SearchRepository(ISession session) : base(session) { }

        public IQueryable<T> Search(TD sParams, bool useKeywordRanking)
        {

            //base: filter out any non-active items 
            //var filterList = new List<Expression<Func<T, bool>>>();
            Expression<Func<T, bool>> filterExpr = x => x.Status.Id == 1 && x.Product.WebEnabled && x.Product.IsSold;

            var qRes = Context.QueryOver<T>().List<T>();

            var res = sParams.IsKeywordSearch 
                ? qRes.Keyword(Context, sParams.SearchText, useKeywordRanking).AsQueryable().Filter(filterExpr)
                : qRes.Filter(filterExpr);

            if (sParams.IsDateSearch)
                 res = DateFilter(res, sParams);

            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < Context.GetActiveDbEducationCategoryCount())
                res = CreditTypeFilter(res, sParams);


            //TODO: Gut enum
            if (sParams.IsZipSearch)
                res = ZipCodeFilter(res, sParams);


            if (sParams.HasLevels)
               res= LevelsFilter(res,sParams);

            res = MeetingTypesFilter(res, sParams);

            //filterExpr = filterList.Aggregate(filterExpr, (current, expression) => current.AndCombine(expression));

           //if results !ranked by keyword and !start/end date search, orderby date desc
            res = !useKeywordRanking ? res.OrderByDescending(x => x.EndDate) : res;

            return res;
        }


        #region Filters

        private IQueryable<T> ZipCodeFilter(IQueryable<T> res, TD sParams)
        {
            var addressIdsByZip = Context.GetMeetingIdByZipDistance(sParams.Zip,
                                                     sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            Expression<Func<T, bool>> expr = x => addressIdsByZip.Contains(x.Id);

            return res.Filter(expr);
        }

        private IQueryable<T> MeetingTypesFilter(IQueryable<T> res, TD sParams)
        {
            Expression<Func<T, bool>> expr;

            //TODO: Gut enum use
            if (sParams.HasMeetingTypes && sParams.MeetingTypes.Count < Context.GetActiveDbMeetingTypesCount())
            {
                expr = x => sParams.MeetingTypes.Any(y => x.Type != null && y.Id == x.Type.Id);
            }
            else
                expr = x => x.Id != (int)EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.Session;

            return res.Filter(expr);
        }

        private static IQueryable<T> LevelsFilter(IQueryable<T> res, TD sParams)
        {
            Expression<Func<T, bool>> expr = x => sParams.Levels.Contains(x.ClassLevelId);

            return res.Filter(expr);
        }

        private IQueryable<T> CreditTypeFilter(IQueryable<T> res, TD sParams)
        {
            //grab selected ids
            var m = Context.GetMeetingIdsInEducationUnitCategories(sParams);

            Expression<Func<T, bool>> expr = x => m.Contains(x.Id);

            return res.Filter(expr);
        }

        private static IQueryable<T> DateFilter(IQueryable<T> res, TD sParams)
        {
            //set to handle only start, only end or ranged date filter
            var sDate = sParams.StartDate.HasValue ? sParams.StartDate.Value : DateTime.Now;
            var eDate = sParams.EndDate.HasValue ? sParams.EndDate.Value : DateTime.MaxValue;

            //filter endDates that fall outside the specified range
            //exclude self study

            //TODO: Remove hardcode. Store business rules in db?
            //var minDays = EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy.GetMinDays();
            
            //Using IsInRange extension method not supported
            //nor is GetLengthInDays
            Expression<Func<T, bool>> expr = x =>
                                             (x.EndDate >= sDate && x.EndDate <= eDate);

            //self study (lengh >= 7 days)
            //Expression<Func<T, bool>> expr2 = x => (x.StartDate - eDate).TotalDays >= minDays;
            //expr = expr.OrCombine(expr2);

            return res.Filter(expr);
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