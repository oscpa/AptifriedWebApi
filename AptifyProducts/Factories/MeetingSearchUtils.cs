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


		private enum KeywordSearchMethodology {
			METHOD_SIMPLETON,
			METHOD_INTELLIGENTIA
		}


        public static MeetingSearchUtilResults BuildFullQuery(AptifriedMeetingSearchDto search) {
            return BuildFullQuery(search, false);
        }

        public static MeetingSearchUtilResults BuildFullQuery(AptifriedMeetingSearchDto search, bool justCounts) {
            string fullQuery = string.Empty;
            Dictionary<string, object> queryParams = new Dictionary<string, object>();

            if (search != null) {
                StringBuilder searchSelect = new StringBuilder("select");
                StringBuilder searchWhere = new StringBuilder();
                StringBuilder searchFrom = new StringBuilder();
				StringBuilder searchOrderBy = new StringBuilder();

                if (justCounts) {
                    searchSelect.AppendLine(" count(mt.id)");
                } else {
					searchSelect.AppendLine(" mt.ID, mt.MeetingTitle, mt.StartDate, mt.EndDate, mt.OpenTime, mt.ClassLevelID, mt.ProductID, mt.StatusID, mt.MeetingTypeID, mt.AddressID, mt.VenueID");

                    // Sort by the meeting end date unless we're doing a keyword search (logic more complex now)
                    if (string.IsNullOrEmpty(search.SearchText)) {
						searchOrderBy.AppendLine(" order by mt.EndDate ");
                    }

                }
                searchWhere.AppendLine(" where mt.StatusID = 1 and mt.WebEnabled = 1 and mt.IsSold = 1 ");

				/**
				 * Ensure that things that have started aren't displayed, unless they're self-study products
				 * (which go on forever) -- so we do a really stupid comparison on them, to ensure they
				 * start on or after when they start.
				 **/
				searchWhere.AppendLine(" and mt.StartDate >= case when mt.MeetingTypeID <> 5 then getdate() else mt.StartDate end ");

				// Because we are getting SIGNIFICANTLY more complex in our handling of keywords, this has been extracted out into its own method
				HandleQueryKeywords(search, queryParams, searchFrom, searchOrderBy, KeywordSearchMethodology.METHOD_INTELLIGENTIA, justCounts);

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

					/**
					 * This addresses OSCPA/Store-Pathfix#163. If "all" the credit types are populated then we'll
					 * just forget about this altogether.
					 * 
					 * At the same time, we'll look for >= 0 when it isn't everything, so we can support partial
					 * credits down the line, if that matters.
					 * **/

					if (search.CreditTypes.Count < 6) {
						// Not ideal to do it with a hardcoded count, I know
						searchWhere.AppendFormat("and mt.ID in(select mtu.MeetingID from dbo.vwMeetingEducationUnits mtu " +
								" where mtu.EducationCategoryID in ({0}) and mtu.EducationUnits >= 0) ", creditTypeIdBuilder.ToString());
					}
                    
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
                            levelInList.Append("0, 2, ");

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

		private static void HandleQueryKeywords(AptifriedMeetingSearchDto search, Dictionary<string, object> queryParams, StringBuilder searchFrom, StringBuilder searchOrderBy, KeywordSearchMethodology howSmart, bool justCounts) {
			if (!string.IsNullOrEmpty(search.SearchText)) {
				if (howSmart == KeywordSearchMethodology.METHOD_SIMPLETON) {
					//searchFrom.AppendFormat(" From freetexttable(idxVwWebSearchIndex, TextContent, '{0}') idx ",
					//    search.SearchText);
					searchFrom.AppendLine(" From freetexttable(idxVwWebSearchIndex, TextContent, :searchText) idx ");
					searchFrom.AppendLine(" join dbo.vwWebSearchIndex vw on idx.[KEY] = vw.ID and vw.EntityID = 980 ");
					searchFrom.AppendLine(" join dbo.vwMeetingsTiny mt on mt.ID = vw.EntityRecordID ");
					queryParams.Add("searchText", search.SearchText);
				} else if (howSmart == KeywordSearchMethodology.METHOD_INTELLIGENTIA) {
					/**
					 * Create from a search string of "term1 term2 ... termn" the string
					 * "term1 near term2 ... near termn."
					 * 
					 * This could more flexibly be generated in the future by doing, e.g.,
					 * "term1 near term2 weight(x) ...," or other SQL methods.
					 * 
					 * http://msdn.microsoft.com/en-us/library/ms189760(v=SQL.90).aspx
					 **/
					string searchStringContainsTable = '(' + search.SearchText.Split(' ')
						.Aggregate(string.Empty, (x, n) => x + (!string.IsNullOrEmpty(x) ? " near " : string.Empty) + n) + ')';

					queryParams.Add("searchStringContainsTables", searchStringContainsTable);
					queryParams.Add("searchStringFreeTextTables", search.SearchText);

					/**
					 * Create the names of the tables, each with their own view of the rankings
					 * based on the search input, and use some arbitrary mappings of how much
					 * importance each should be judged to have.
					 * 
					 * After creating this mapping, create the derived rank calculation in the select above.
					 * 
					 * In the future, these should be derived evolutionarily or by some other ML method.
					 **/
					IDictionary<string, UInt32> subrankMaps = new Dictionary<string, UInt32> {
						{"snProductName",			25},
						{"snSearchableID",			25},
						{"snWhoShouldPurchase",		1},
						{"snSummary",				10},
						{"snObjective",				15},
						{"snAddtionalInformation",	1},
						{"snCreditTypes",			1},
						{"snSpeakers",				25},
						{"sfProductName",			25},
						{"sfWhoShouldPurchase",		1},
						{"sfSummary",				10},
						{"sfObjective",				15},
						{"sfAddtionalInformation",	1},
						{"sfCreditTypes",			1},
						{"snVenueName",				1},
						{"snVenueCity",				10},
						{"snVendor",				3},
						{"snWebKeywords",			30},
						{"sfMeetingType",			5}
					};

					if (!justCounts) {
						// We don't need to worry about clobbering any sort logic here because it won't have been defined yet
						searchOrderBy.AppendLine("order by");

						LinkedList<string> rankStatements = new LinkedList<string>();
						IEnumerator<KeyValuePair<string, uint>> rankEnumer = subrankMaps.GetEnumerator();
						while (rankEnumer.MoveNext()) {
							rankStatements.AddLast(rankEnumer.Current.Value + " * isnull(" + rankEnumer.Current.Key + ".Rank, 0)");
						}
						searchOrderBy.AppendLine(rankStatements.Aggregate(string.Empty, (x, n) => x + (!string.IsNullOrEmpty(x) ? " + " : string.Empty) + n));

						searchOrderBy.AppendLine("desc, mt.StartDate, isnull(mt.ParentID, 0) asc");
					}

					searchFrom.AppendLine("from vwMeetingsTiny mt");
					searchFrom.AppendLine("inner join vwStoreSearches s on s.ProductID = mt.ProductID");

					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, ProductName, :searchStringContainsTables  ) snProductName on snProductName.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, WhoShouldPurchase, :searchStringContainsTables  ) snWhoShouldPurchase on snWhoShouldPurchase.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, Summary, :searchStringContainsTables  ) snSummary on snSummary.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, Objective, :searchStringContainsTables  ) snObjective on snObjective.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, AddtionalInformation, :searchStringContainsTables  ) snAddtionalInformation on snAddtionalInformation.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, MeetingType, :searchStringContainsTables  ) snMeetingType on snMeetingType.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, Level, :searchStringContainsTables  ) snLevel on snLevel.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, CreditTypes, :searchStringContainsTables ) snCreditTypes on snCreditTypes.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, Speakers, :searchStringContainsTables  ) snSpeakers on snSpeakers.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, Vendor, :searchStringContainsTables  ) snVendor on snVendor.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, VenueCity, :searchStringContainsTables  ) snVenueCity on snVenueCity.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, VenueName, :searchStringContainsTables  ) snVenueName on snVenueName.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, WebKeyWords, :searchStringContainsTables  ) snWebKeyWords on snWebKeyWords.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN CONTAINSTABLE(idxvwStoreSearch, SearchableID, :searchStringContainsTables  ) snSearchableID on snSearchableID.[KEY] = s.ID");

					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, ProductName, :searchStringFreeTextTables) sfProductName on sfProductName.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, WhoShouldPurchase, :searchStringFreeTextTables  ) sfWhoShouldPurchase on sfWhoShouldPurchase.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, Summary, :searchStringFreeTextTables  ) sfSummary on sfSummary.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, Objective, :searchStringFreeTextTables  ) sfObjective on sfObjective.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, AddtionalInformation, :searchStringFreeTextTables  ) sfAddtionalInformation on sfAddtionalInformation.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, MeetingType, :searchStringFreeTextTables  ) sfMeetingType on sfMeetingType.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, Level, :searchStringFreeTextTables  ) sfLevel on sfLevel.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, CreditTypes, :searchStringFreeTextTables ) sfCreditTypes on sfCreditTypes.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, Speakers, :searchStringFreeTextTables  ) sfSpeakers on sfSpeakers.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, Vendor, :searchStringFreeTextTables  ) sfVendor on sfVendor.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, VenueCity, :searchStringFreeTextTables  ) sfVenueCity on sfVenueCity.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, VenueName, :searchStringFreeTextTables  ) sfVenueName on sfVenueName.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, WebKeyWords, :searchStringFreeTextTables  ) sfWebKeyWords on sfWebKeyWords.[KEY] = s.ID");
					searchFrom.AppendLine("LEFT JOIN FREETEXTTABLE(idxvwStoreSearch, SearchableID, :searchStringFreeTextTables  ) sfSearchableID on sfSearchableID.[KEY] = s.ID");
				}
			} else {
				searchFrom.AppendLine(" From dbo.vwMeetingsTiny mt ");
			}
		}
    }
}