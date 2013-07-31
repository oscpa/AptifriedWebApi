using System.Web.Mvc;
using System.Web.Services.Protocols;

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

//In person and online (1,2) are returning DOMA result. Ondemand only has 1 result 

//TODO: Index for returning from product page back to product in search list
//set the hash to page-productid and hash link each product

//TODO: Tabs for each MeetingTypeGroup
//TODO: Sub-Grouping
//TODO: Export search to excel
//TODO: Filter - Date sort order (from search obj)

//TODO: IP geolocation

//TODO: Check zipcode search result accuracy

namespace AptifyWebApi.Repository
{
    public class SearchRepository<T, TD> : NHibernateBaseRepository<ISession, T>
        where T : AptifriedMeeting
        where TD : AptifriedMeetingSearchDto
    {
        public SearchRepository(ISession session) : base(session) { }

        public IQueryable<T> Search(TD sParams, bool useKeywordRanking)
        {
            var f = GetFilter(sParams);

            var res =
               Context.QueryOver<T>()
                   .Keyword(Context, sParams.SearchText, useKeywordRanking)
                   .Filter(f);

            //if results !ranked by keyword and !start/end date search, orderby date desc
            return !useKeywordRanking ? res.OrderBy(x => x.EndDate) : res;
        }

        private Expression<Func<T, bool>> GetFilter(TD sParams)
        {
        //base: filter out any non-active items 
           
            var filterList = new List<Expression<Func<T, bool>>>();
            
            Expression<Func<T, bool>> filterExpr = x => x.Status.Id == 1 
                && x.Product.WebEnabled 
                && x.Product.IsSold;

            filterList.Add(filterExpr);

            if (sParams.IsDateSearch)
                  filterList.Add(DateFilter(sParams));

            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < Context.GetActiveDbEducationCategoryCount())
                 filterList.Add(CreditTypeFilter(sParams));

            
            //TODO: Gut enum
            if (sParams.IsZipSearch)
                filterList.Add(ZipCodeFilter(sParams));


            if (sParams.HasLevels)
               filterList.Add(LevelsFilter(sParams));

            
            filterList.Add(MeetingTypesGroupFilter(sParams));

            filterList.Add(MeetingTypesFilter(sParams));

            filterExpr = filterList.Aggregate(filterExpr, (current, expression) => current.AndCombine(expression));

           

            return filterExpr;
        }


        #region Filters

        private Expression<Func<T, bool>> ZipCodeFilter(TD sParams)
        {
            var addressIdsByZip = Context.GetMeetingIdByZipDistance(sParams.Zip,
                                                     sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            Expression<Func<T, bool>> expr = x => x.TypeItem.Group.Id == (int)EnumsAndConstants.MeetingTypeGroup.InPerson && addressIdsByZip.Contains(x.Id);

            return expr;
        }

        private Expression<Func<T, bool>>  MeetingTypesFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr;

            if (sParams.HasMeetingTypes && sParams.MeetingTypes.Count < Context.GetActiveDbMeetingTypesCount())
            {
                expr = x => sParams.MeetingTypes.Any(y => x.TypeItem.IsNotNull() && x.TypeItem.Type.IsNotNull() && x.TypeItem.Type.IsNotNull() && y.Type.Id == x.TypeItem.Type.Id);
            }
            else
                expr = x => x.Id != (int)EnumsAndConstants.MeetingType.Session;

            return expr;
        }

        private Expression<Func<T, bool>> MeetingTypesGroupFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr;

            if (sParams.HasTypeGroups)
            {
                expr = x => sParams.MeetingTypes.Any(y => x.TypeItem.IsNotNull() && x.TypeItem.Group.IsNotNull() && x.TypeItem.Group.Id.IsNotNull() && y.Group.Id == x.TypeItem.Group.Id);
            }
            else
                expr = x => x.Id != (int)EnumsAndConstants.MeetingType.Session;

            return expr;
        }

        private static Expression<Func<T, bool>> LevelsFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr = x => sParams.Levels.Contains(x.ClassLevelId);

            return expr;
        }

        private Expression<Func<T, bool>> CreditTypeFilter(TD sParams)
        {
            //grab selected ids
            var mIds = Context.GetMeetingIdsInEducationUnitCategories(sParams);

            if(mIds.IsNull())
            ;
            Expression<Func<T, bool>> expr = x => mIds.Contains(x.Id);

            return expr;
        }

        private static Expression<Func<T, bool>> DateFilter(TD sParams)
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