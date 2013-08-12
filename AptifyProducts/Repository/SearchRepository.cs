using System.Data.Common.EntitySql;
using System.Net;
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

/*
 
We've made it easier to find products by category (We can adjust the types of categories we use when we have new groupings)
We've simplified the filters as members don't use them
Items in tabe will order as they should and have sort filters specific to them
In person order by options = Date, Distance, Relevance
Online = Date Relevance
Self study = Relevance  
We've removed infinite scroll and added pagination 
We've added a large box to ensure that other options are known even if tabs are ignored

Search Improvements 
    DONE:   Tabbed results list by product grouping
    DONE:   Initial search results load will be populated with a list of courses within 60 miles of the users current location as priority items
            Filter changes based on the type of events you are looking at (Example remove radius id viewing on-line or on-demand)
            Export results list to excel
    DONE:   Direct link to a results list from a custom URL

 */

//---------------------------------------------------------------------

//TODO: Filter - Date sort order (from search obj/enum)
//  each tab has dropdown to order by dynamic x,y,z

//TODO: Export search to excel

//---------------------------------------------------------------------

//WBN: Sub-Grouping (Parent/Child)
//WBN: Show loc1/loc2 directions map
//WBN: Return sessions.parent.parent in keyword search
//WBN: IP geolocation
//WBN: Index for returning from product page back to product in search list

//---------------------------------------------------------------------

//add tracking to pagination links

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

            var filterExpr = BaseFilter(sParams);
            
            filterList.Add(filterExpr);

            if (sParams.IsDateSearch)
                  filterList.Add(DateFilter(sParams));

            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < Context.GetActiveDbEducationCategoryCount())
                 filterList.Add(CreditTypeFilter(sParams));

            
            if (sParams.IsZipSearch)
                filterList.Add(ZipCodeFilter(sParams));


            if (sParams.HasLevels)
               filterList.Add(LevelsFilter(sParams));


            if (sParams.HasTypeGroups)
                filterList.Add(MeetingTypesGroupFilter(sParams));


            if (sParams.HasMeetingTypes && NotAllTypeOptionsSelected(sParams))
                filterList.Add(MeetingTypesFilter(sParams));


            filterList.Add(SessionFilter());

            filterExpr = filterList.Aggregate(filterExpr, (current, expression) => current.AndAlsoCombine(expression));

           

            return filterExpr;
        }

        #region Filters

        private static Expression<Func<T, bool>> SessionFilter()
        {
            Expression<Func<T, bool>> expr = x => x.TypeItem.Type.Id != (int) EnumsAndConstants.MeetingType.Session;

            return expr;
        }

        private static Expression<Func<T, bool>> BaseFilter(TD sParam)
        {   
            var sDate = sParam.StartDate ?? DateTime.Now;
            Expression<Func<T, bool>> expr = x => x.Status.Id == 1
                                                  && x.Product.WebEnabled
                                                  && x.Product.IsSold
                                                  && x.TypeItem != null
                                                  && (x.StartDate >= sDate | x.TypeItem.Group.Id == (int)EnumsAndConstants.MeetingTypeGroup.SelfStudy)
                                                  ;
                                                  

            return expr;
        }

        private Expression<Func<T, bool>> ZipCodeFilter(TD sParams)
        {
            var addressIdsByZip = Context.GetMeetingIdByZipDistance(sParams.Zip,
                                                     sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            //Expression<Func<T, bool>> expr = x => x.TypeItem.Group.Id == (int)EnumsAndConstants.MeetingTypeGroup.InPerson && addressIdsByZip.Contains(x.Id);
            Expression<Func<T, bool>> expr = x => x.TypeItem.Group.Id == (int)EnumsAndConstants.MeetingTypeGroup.InPerson && addressIdsByZip.Contains(x.Id);
                //Select(x => { x.Miles = "foo"; return x; })

            return expr;
        }

        private static Expression<Func<T, bool>>  MeetingTypesFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr = x => sParams.MeetingTypes.Any(y => x.TypeItem.IsNotNull()
                                                                                && x.TypeItem.Type.IsNotNull() &&
                                                                                x.TypeItem.Type.Id.IsNotNull()
                                                                                && y.Type.Id == x.TypeItem.Type.Id);

           //Expression<Func<T, bool>> expr2 = x => x.TypeItem.Type.Id != (int) EnumsAndConstants.MeetingType.Session;
           
            return expr;
        }

        private static Expression<Func<T, bool>> MeetingTypesGroupFilter(TD sParams)
        {
            Expression<Func<T, bool>> expr = x => sParams.MeetingTypes.Any(y =>
                x.TypeItem.IsNotNull() && x.TypeItem.Group.IsNotNull() && x.TypeItem.Group.Id.IsNotNull() 
            && y.Group.Id == x.TypeItem.Group.Id);
           
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
         
            //Using IsInRange extension method not supported
            //nor is GetLengthInDays
            Expression<Func<T, bool>> expr = x =>
                                             (x.EndDate >= sDate && x.EndDate <= eDate);


            //TODO: Add self-study exclusion
            //self study (lengh >= 7 days)
            //Expression<Func<T, bool>> expr2 = x => (x.StartDate - eDate).TotalDays >= minDays;
            //expr = expr.OrCombine(expr2);

            return expr;
        }


        #endregion


        #region Get

        private bool NotAllTypeOptionsSelected(AptifriedMeetingSearchDto sParams)
        {
            return sParams.MeetingTypes.Count < Context.GetActiveDbMeetingTypesCount();
        }

        public T GetSingle(int id)
        {
            Expression<Func<T, bool>> expr = x => x.Id == id;

            return FindBy(expr).Single();
        }
        #endregion
    }

    //end class
}

//end namespace