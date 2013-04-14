using AptifyWebApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AptifyWebApi.Factories {
    internal static class MeetingSearchUtils {

        internal class MeetingSearchUtilResults {
            public string FullQuery { get; set; }
            public Dictionary<string, object> QueryParams { get; set; }
        }


        public static MeetingSearchUtilResults BuildFullQuery(AptifriedMeetingSearchDto search) {
            return BuildFullQuery(search, false);
        }

        public static MeetingSearchUtilResults BuildFullQuery(AptifriedMeetingSearchDto search, bool justCounts) {
            string fullQuery = string.Empty;
            Dictionary<string, object> queryParams = new Dictionary<string, object>();

            if (search != null) {
                StringBuilder searchSelect = new StringBuilder();
                StringBuilder searchWhere = new StringBuilder();
                StringBuilder searchFrom = new StringBuilder();
                StringBuilder searchOrderBy = new StringBuilder();

                if (justCounts) {
                    searchSelect.Append(" Select count(mt.id) ");
                } else {
                    searchSelect.Append(" Select mt.* ");
                }
                searchWhere.AppendLine(" where 1=1 and mt.StatusID = 1 and mt.WebEnabled = 1 and mt.IsSold = 1 ");

                searchOrderBy.AppendLine(" order by mt.EndDate ");

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
                    searchWhere.AppendLine(" and mt.EndDate between :beginDate and :endDate ");
                    //searchWhere.AppendFormat(" and mt.StartDate between '{0}' and '{1}' ",
                    //    search.StartDate.Value,
                    //    search.EndDate.Value.AddHours(23));
                    queryParams.Add("beginDate", search.StartDate.Value);
                    queryParams.Add("endDate", search.EndDate.Value);

                } else {

                    if (search.StartDate.HasValue) {
                        searchWhere.AppendLine(" and mt.EndDate >= :beginDate ");
                        //searchWhere.AppendFormat(" and Class.[Start Date] >= '{0}' ",
                        //    search.StartDate.Value);
                        queryParams.Add("beginDate", search.StartDate.Value);
                    }
                    if (search.EndDate.HasValue) {
                        searchWhere.AppendLine(" and mt.EndDate <= :endDate ");
                        //searchWhere.AppendFormat(" and Class.[Start Date] <= '{0}' ",
                        //    search.EndDate.Value.AddHours(23));
                        queryParams.Add("endDate", search.EndDate.Value);
                    }
                }

                if (search.CreditTypes != null && search.CreditTypes.Count > 0) {
                    StringBuilder creditTypeIdBuilder = new StringBuilder();
                    for (int i = 0; i < search.CreditTypes.Count; i++) {
                        if (creditTypeIdBuilder.Length > 0)
                            creditTypeIdBuilder.Append(",");

                        creditTypeIdBuilder.Append(search.CreditTypes[i].Id.ToString());
                    }
                    searchWhere.AppendFormat("and mt.ID in(select mtu.MeetingID from dbo.vwMeetingEducationUnits mtu " +
                            " where mtu.EducationCategoryID in ({0}) and mtu.EducationUnits >= 1) ", creditTypeIdBuilder.ToString());
                    
                }

				if (!string.IsNullOrEmpty(search.Zip) && search.MilesDistance > 0 
                    && (search.MeetingType.Contains("InPerson") || search.MeetingType.Contains("All"))) {

                    searchFrom.AppendLine(" join dbo.vwAddressesTiny at on mt.AddressID = at.ID ");
                    searchWhere.AppendLine("  and exists (select * from fnOSCPAGetZipDistanceWeb(:zipCode, at.PostalCode) dt where dt.Distance <= :milesDistance)");
                    //searchWhere.AppendFormat("  and exists (select * from fnOSCPAGetZipDistanceWeb('{0}', at.PostalCode) dt where dt.Distance <= {1})",
                    //    search.Zip,
                    //    search.MilesDistance);
                    queryParams.Add("zipCode", search.Zip);
                    queryParams.Add("milesDistance", search.MilesDistance);
                }

                if (search.Levels != null && search.Levels.Count > 0) {

                    StringBuilder levelInList = new StringBuilder();
                    foreach (int levelId in search.Levels) {
                        if (levelInList.Length > 0)
                            levelInList.Append(",");
                        else
                            levelInList.Append("0,");

                        levelInList.Append(levelId.ToString());
                    }
                    searchWhere.AppendFormat(" and mt.ClassLevelID in ( {0} ) ",
                        levelInList.ToString());

                }

                // TODO : UN-hard-code these.. too much maintenance
                if (search.MeetingType != null && search.MeetingType.Count > 0) {
                    StringBuilder meetingTypesTofilter = new StringBuilder();
                    foreach (var meetingType in search.MeetingType) {
                        if (meetingType == "InPerson") {
                            if (meetingTypesTofilter.Length > 0) {
                                meetingTypesTofilter.Append(",");
                            }
                            meetingTypesTofilter.Append("3, 4, 8, 9");
                        }
                        if (meetingType == "OnLine") {
                            if (meetingTypesTofilter.Length > 0) {
                                meetingTypesTofilter.Append(",");
                            }
                            meetingTypesTofilter.Append("2, 7");
                        }
                        if (meetingType == "SelfStudy") {
                            if (meetingTypesTofilter.Length > 0) {
                                meetingTypesTofilter.Append(",");
                            }
                            meetingTypesTofilter.Append("5");
                        }
                    }

                    searchWhere.AppendFormat(" and mt.MeetingTypeID in ({0})",
                        meetingTypesTofilter.ToString());
                } else {
                    // remove sessions from the list regardless.
                    searchWhere.Append(" and mt.MeetingTypeID not in (6) ");
                }

                fullQuery = searchSelect.ToString() +
                                    searchFrom.ToString() +
                                    searchWhere.ToString() +
                                    searchOrderBy.ToString();

            }

            return new MeetingSearchUtilResults() {
                FullQuery = fullQuery,
                QueryParams = queryParams
            };
        }
    }
}