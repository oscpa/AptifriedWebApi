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

        public IQueryable<T> Search(TD sParams, bool useKeywordRanking)
        {

            //base: filter out any non-active items 
            //var filterList = new List<Expression<Func<T, bool>>>();

            Expression<Func<T, bool>> filterExpr = x => x.Status.Id == 1 && x.Product.WebEnabled && x.Product.IsSold;

            var qRes = Context.QueryOver<T>().List<T>();

            var res = sParams.IsKeywordSearch 
                ? qRes.Keyword(Context, sParams.SearchText, useKeywordRanking).AsQueryable().Filter(filterExpr)
                : qRes.Filter(filterExpr);

            qRes.Clear();

            if (sParams.IsDateSearch)
                 res = DateFilter(res, sParams);

            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < Context.GetActiveDbEducationCategoryCount())
                res = CreditTypeFilter(res, sParams);


            //TODO: Gut enum
            if (sParams.IsZipSearch) // && sParams.HasMeetingTypes)
            {
                /*
                var isIn =
                    sParams.MeetingType.Where(
                        x =>
                        x == 
                            EnumsAndConstantsToAvoidDatabaseChanges.MeetingTypeCategory.InPerson.Description()));

                if (isIn || sParams.MeetingTypes.Count() == Context.GetActiveDbMeetingTypesCount())
                                      */
                res = ZipCodeFilter(res, sParams);

            }


            if (sParams.HasLevels)
               res= LevelsFilter(res,sParams);

            res = MeetingTypesFilter(res, sParams);

            //filterExpr = filterList.Aggregate(filterExpr, (current, expression) => current.AndCombine(expression));

       

            //if results !ranked by keyword and !start/end date search, orderby date desc
            return !useKeywordRanking ? res.OrderByDescending(x => x.EndDate) : res;
        }


        #region Filters

        private IQueryable<T> ZipCodeFilter(IQueryable<T> res, TD sParams)
        {
            var zips = Context.GetZipDistanceWeb(sParams.Zip,
                                                     sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            Expression<Func<T, bool>> expr = x => zips.Contains(x.Location.Id);

            return res.Filter(expr);
        }

        private IQueryable<T> MeetingTypesFilter(IQueryable<T> res, TD sParams)
        {
            Expression<Func<T, bool>> expr;

            //TODO: Use meeting type objects
            //TODO: Gut enum use
            if (sParams.HasMeetingTypes && sParams.MeetingType.Count < Context.GetActiveDbMeetingTypesCount())
            {
                expr = x => sParams.MeetingType.Contains(x.Id);
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

            //TODO: Gut enum
            //filter endDates that fall outside the specified range
            //exclude self study


            const int minDays = 7; // EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy.GetMinDays();
            
            //Using IsInRange extension method not supported
            //nor is GetLengthInDays
            Expression<Func<T, bool>> expr = x =>
                                             (x.EndDate >= sDate && x.EndDate <= eDate);

            Expression<Func<T, bool>> expr2 = x =>
            ((x.StartDate - eDate).TotalDays >= minDays);

            expr = expr.OrCombine(expr2);

            return res.Filter(expr);

            //x.Id == (int) EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy;
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