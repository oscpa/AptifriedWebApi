#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingTextSearchController : AptifyEnabledApiController
    {
        public AptifriedMeetingTextSearchController(ISession session) : base(session)
        {
        }

        public IList<AptifriedMeetingTextSearchResultDto> Get(string search)
        {
            if (string.IsNullOrEmpty(search))
                throw new HttpException(500, "Must contain text.");

            return GetSearchResults(search);
        }

        private IList<AptifriedMeetingTextSearchResultDto> GetSearchResults(string search)
        {
            const string fullTextIndexQuery = "SELECT mt.ID, mt.MeetingTitle, idx.[RANK] " +
                                              " FROM freetexttable(idxVwWebSearchIndex, TextContent, :search) idx " +
                                              " join dbo.vwWebSearchIndex vw on idx.[KEY] = vw.ID " +
                                              " join dbo.vwMeetingsTiny mt on mt.ID = vw.EntityRecordID " +
                                              " where EntityID = 980 " +
                                              " and mt.StartDate >= GetDate() " +
                                              " order by idx.[RANK] desc ";
            var textSearchResult = session.CreateSQLQuery(fullTextIndexQuery)
                                          .SetParameter("search", search)
                                          .List();

            // TODO: try to work in a way of strongly typing a collection

            return (from object[] item in textSearchResult
                    select new AptifriedMeetingTextSearchResultDto
                        {
                            Id = Convert.ToInt32(item[0]),
                            Name = Convert.ToString(item[1]),
                            Rank = Convert.ToInt32(item[2])
                        }).ToList();
        }
    }
}