using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingSearchController : AptifyEnabledApiController {

        public AptifriedMeetingSearchController(ISession session) : base(session) { }

        public IList<AptifriedMeetingDto> Post(AptifriedMeetingSearchDto search) {
            IList<AptifriedMeetingDto> resultingMeetings = new List<AptifriedMeetingDto>();

            if (search != null) {
                StringBuilder searchSelect = new StringBuilder();
                StringBuilder searchWhere = new StringBuilder();
                StringBuilder searchFrom = new StringBuilder();

                searchSelect.Append(" Select mt.* ");
                searchWhere.AppendLine(" where 1=1 ");

                Dictionary<string, object> queryParams = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(search.SearchText)) {
                    //searchFrom.AppendFormat(" From freetexttable(idxVwWebSearchIndex, TextContent, '{0}') idx ",
                    //    search.SearchText);
                    searchFrom.AppendLine(" From freetexttable(idxVwWebSearchIndex, TextContent, :searchText) idx ");
                    searchFrom.AppendLine(" join dbo.vwWebSearchIndex vw on idx.[KEY] = vw.ID and vw.EntityID = 980 ");
                    searchFrom.AppendLine(" join dbo.vwMeetingsTiny mt on mt.ID = vw.EntityRecordID ");

                    queryParams.Add("searchText", search.SearchText);
                } else {
                    searchFrom.AppendLine(" From dbo.vwMeetingsTiny mt ");
                }

                if (search.StartDate.HasValue && search.EndDate.HasValue) {
                    searchWhere.AppendLine(" and mt.StartDate between :beginDate and :endDate ");
                    //searchWhere.AppendFormat(" and mt.StartDate between '{0}' and '{1}' ",
                    //    search.StartDate.Value,
                    //    search.EndDate.Value.AddHours(23));
                    queryParams.Add("beginDate", search.StartDate.Value);
                    queryParams.Add("endDate", search.EndDate.Value);

                } else {

                    if (search.StartDate.HasValue) {
                        searchWhere.AppendLine(" and mt.StartDate >= :beginDate ");
                        //searchWhere.AppendFormat(" and Class.[Start Date] >= '{0}' ",
                        //    search.StartDate.Value);
                        queryParams.Add("beginDate", search.StartDate.Value);
                    }
                    if (search.EndDate.HasValue) {
                        searchWhere.AppendLine(" and mt.StartDate <= :endDate ");                    
                        //searchWhere.AppendFormat(" and Class.[Start Date] <= '{0}' ",
                        //    search.EndDate.Value.AddHours(23));
                        queryParams.Add("endDate", search.EndDate.Value);
                    }
                }

                if (search.CreditTypes != null && search.CreditTypes.Count > 0) {
                    for (int i = 0; i < search.CreditTypes.Count; i++) {
                        searchWhere.AppendFormat("and mt.ID in(select mtu.MeetingID from dbo.vwMeetingEducationUnits mtu " + 
                            " where mtu.EducationCategoryID = :categoryId{0} and mtu.EducationUnits > :units{0}) ", i);

                        queryParams.Add("categoryId" + i.ToString(), search.CreditTypes[i].Id);
                        queryParams.Add("units" + i.ToString(), search.CreditTypes[i].EducationUnits);
                    }
                }

                if (!string.IsNullOrEmpty(search.Zip) && search.MilesDistance > 0) {
                    searchFrom.AppendLine(" join dbo.vwAddressesTiny at on mt.AddressID = at.ID ");
                    searchWhere.AppendLine("  and exists (select * from fnOSCPAGetZipDistanceWeb(:zipCode, at.PostalCode) dt where dt.Distance <= :milesDistance)");
                    //searchWhere.AppendFormat("  and exists (select * from fnOSCPAGetZipDistanceWeb('{0}', at.PostalCode) dt where dt.Distance <= {1})",
                    //    search.Zip,
                    //    search.MilesDistance);
                    queryParams.Add("zipCode", search.Zip);
                    queryParams.Add("milesDistance", search.MilesDistance);
                }
                string fullQuery = searchSelect.ToString() + searchFrom.ToString() + searchWhere.ToString();
                var meetingQuery = session.CreateSQLQuery(fullQuery)
                    .AddEntity("mt", typeof(AptifriedMeeting));
                foreach (string paramKey in queryParams.Keys) {
                    meetingQuery.SetParameter(paramKey, queryParams[paramKey]);
                }

                var meetings = meetingQuery.List<AptifriedMeeting>();

                resultingMeetings = Mapper.Map(meetings, new List<AptifriedMeetingDto>());
            }

            return resultingMeetings;
        }


    }
}