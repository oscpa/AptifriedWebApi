<<<<<<< HEAD
﻿#region using

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Shared;
using NHibernate;
using NHibernate.Linq;

using MeetingSearchDto = AptifyWebApi.Dto.AptifriedMeetingSearchDto;
using Meeting = AptifyWebApi.Models.AptifriedMeeting;
#endregion

//TODO: Filter - Date sort order (from search obj)

namespace AptifyWebApi.Repository
{
    public class SearchRepository : BaseRepository
    {
        protected readonly MeetingSearchDto SParams;

        public SearchRepository(ISession session) : base(session){}

        public IList<Meeting> Search(MeetingSearchDto sParams,
                                              bool useKeywordRanking)
        {
            //base: filter out any non-active items 
             Expression<Func<Meeting, bool>> filterExpr = x => x.Status.Id == 1 && x.Product.WebEnabled && x.Product.IsSold;

            if (sParams.IsDateSearch)
                filterExpr.AndAlso(DateFilter(sParams));
        
            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < Session.GetActiveDbEducationCategoryCount())
                filterExpr.AndAlso(CreditTypeFilter(sParams));


            //TODO: Gut enum
            if (sParams.IsZipSearch &&
                (sParams.MeetingTypes.Any(x => x.Description.Equals(EnumsAndConstantsToAvoidDatabaseChanges.MeetingTypeCategory.InPerson.Description()))) ||
                 sParams.MeetingTypes.Count() == Session.GetActiveDbMeetingTypesCount())
            {
                filterExpr.AndAlso(ZipCodeFilter(sParams));

            }

            if (sParams.HasLevels)
                filterExpr.AndAlso(LevelsFilter(sParams));

            filterExpr.AndAlso(MeetingTypesFilter(sParams));
              
            //apply keyword search and filter
            var res =
                Session.Query<Meeting>()
                       .Keyword(Session, sParams.SearchText, useKeywordRanking)
                       .Filter(filterExpr);

            //if results !ranked by keyword and !start/end date search, orderby date desc
            return !useKeywordRanking ? res.OrderByDescending(x => x.EndDate).ToList() : res.ToList();

        }


        #region Filters

        private Expression<Func<Meeting, bool>> ZipCodeFilter(MeetingSearchDto sParams)
        {
            var zips = Session.GetZipDistanceWeb(sParams.Zip,
                                                     sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            Expression<Func<Meeting, bool>> expr = x => zips.Contains(x.Location.Id);

            return expr;
        }
        
        private Expression<Func<Meeting, bool>> MeetingTypesFilter(MeetingSearchDto sParams)
        {
            Expression<Func<Meeting, bool>> expr;

            //TODO: Gut enum use
            if (sParams.HasMeetingTypes && sParams.MeetingTypes.Count < Session.GetActiveDbMeetingTypesCount())
            {
                var meetingTypeIds = new List<int>();

                foreach (var mType in sParams.MeetingTypes)
                    meetingTypeIds.AddRange(EnumHelper.GetMeetingTypeIdsByCategoryDescription(mType.Name));

                expr = x => meetingTypeIds.Contains(x.Id);
            }
            else
                expr = x => x.Id != (int)EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.Session;

            return expr;
        }

        private static Expression<Func<Meeting, bool>> LevelsFilter(MeetingSearchDto sParams)
        {
            Expression<Func<Meeting, bool>> expr = x => sParams.Levels.Contains(x.ClassLevelId);

            return expr;
        }

        private static Expression<Func<Meeting, bool>> CreditTypeFilter(MeetingSearchDto sParams)
        {
            //grab selected ids
            var creditTypeIds = sParams.CreditTypes.Select(x => x.Id).Distinct();

            Expression<Func<Meeting, bool>> expr = x => x.Credits.Any(y => creditTypeIds.Contains(y.Id));

            return expr;
        }

        private static Expression<Func<Meeting, bool>> DateFilter(MeetingSearchDto sParams)
        {
                //set to handle only start, only end or ranged date filter
                var sDate = sParams.StartDate.HasValue ? sParams.StartDate.Value : DateTime.Now;
                var eDate = sParams.EndDate.HasValue ? sParams.EndDate.Value : DateTime.MaxValue;

            //TODO: Gut enum
                //filter endDates that fall outside the specified range
                //exclude self study

                Expression<Func<Meeting, bool>> expr = x => 
                                    x.EndDate.IsInRange(sDate, eDate) |
                                    x.StartDate.GetLengthInDays(eDate) >=
                                    EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy.GetMinDays();
                                  //x.Id == (int) EnumsAndConstantsToAvoidDatabaseChanges.MeetingType.SelfStudy;
                return expr;
        }
        #endregion
    }

//end class
}

=======
﻿#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Models.Dto.Meeting;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.Shared;
using NHibernate;
using NHibernate.Linq;

#endregion

//TODO: Filter - Date sort order (from search obj)

//WBN: Refactor if(sParams.<Checks>) into seperate functions for testability
//WBN: Refactor filterExpr assignments into expression builder

namespace AptifyWebApi.Repository
{
    public class SearchRepository
    {
        public IList<AptifriedMeeting> Search(ISession session, AptifriedMeetingSearchDto sParams,
                                              bool useKeywordRanking)
        {
            //base: filter out any non-active items 
            Expression<Func<AptifriedMeeting, bool>> filterExpr =
                x => x.Status.Id == 1 && x.Product.WebEnabled && x.Product.IsSold;

            //apply keyword search and base filter
            var res =
                session.Query<AptifriedMeeting>()
                       .Keyword(session, sParams.SearchText, useKeywordRanking)
                       .Filter(filterExpr);

            if (sParams.IsDateSearch)
            {
                //set to handle only start, only end or ranged date filter
                var sDate = sParams.StartDate.HasValue ? sParams.StartDate.Value : DateTime.Now;
                var eDate = sParams.EndDate.HasValue ? sParams.EndDate.Value : DateTime.MaxValue;

                //filter endDates that fall outside the specified range
                //exclude self study
                filterExpr = x => x.EndDate.IsInRange(sDate, eDate) |
                                  x.Id == (int) Enums.MeetingType.SelfStudy;

                res = res.Filter(filterExpr);
            }

            //if credit type search and not all types selected
            if (sParams.HasCreditTypes && sParams.CreditTypes.Count() < session.GetActiveDbEducationCategoryCount())
            {
                //grab selected ids
                var creditTypeIds = sParams.CreditTypes.Select(x => x.Id).Distinct();

                filterExpr = x => x.Credits.Any(y => creditTypeIds.Contains(y.Id));

                res = res.Filter(filterExpr);
            }

            //TODO: Junk
            //if (sParams.IsZipSearch &&
            //    (sParams.MeetingTypes.Contains( Enums.MeetingTypeCategory.InPerson.Description()) ||
            //     sParams.MeetingTypes.Count() == session.GetActiveDbMeetingTypesCount()))
            //{
            //    var zips = session.GetZipDistanceWeb(sParams.Zip,
            //                                         sParams.MilesDistance.ToString(CultureInfo.InvariantCulture));

            //    filterExpr = x => zips.Contains(x.Location.Id);

            //    res = res.Filter(filterExpr);
            //}

            if (sParams.HasLevels)
            {
                filterExpr = x => sParams.Levels.Contains(x.ClassLevelId);

                res = res.Filter(filterExpr);
            }


            //TODO: Junk
            if (sParams.HasMeetingTypes && sParams.MeetingTypes.Count < session.GetActiveDbMeetingTypesCount())
            {
                var meetingTypeIds = new List<int>();

                foreach (var mType in sParams.MeetingTypes)
                    meetingTypeIds.AddRange(EnumHelper.GetMeetingTypeIdsByCategoryDescription(mType.Name));

                filterExpr = x => meetingTypeIds.Contains(x.Id);
            }
            else
                filterExpr = x => x.Id != (int) Enums.MeetingType.Session;

            res = res.Filter(filterExpr);

            //if results !ranked by keyword and !start/end date search, orderby date desc
            return !useKeywordRanking ? res.OrderByDescending(x => x.EndDate).ToList() : res.ToList();
        }
    }

//end class
}

>>>>>>> origin/ac-init
//end namespace