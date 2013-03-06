﻿using AptifyWebApi.Dto;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    public class AptifriedClassTextSearchController : AptifyEnabledApiController {
        public AptifriedClassTextSearchController(ISession session) : base(session) { }

        public IList<AptifriedClassTextSearchResultDto> Get(string search) {

            if (string.IsNullOrEmpty(search))
                throw new HttpException(500, "Must contain text.");

            return GetSearchResults(search);
        }

        private IList<AptifriedClassTextSearchResultDto> GetSearchResults(string search) {

            string fullTextIndexQuery = "SELECT TOP 100 ct.ID, ct.WebName, idx.[RANK] " +
                " FROM freetexttable(idxVwWebSearchIndex, TextContent, :search) idx " +
                " join dbo.vwWebSearchIndex vw on idx.[KEY] = vw.ID " + 
                " join dbo.vwClassesTiny ct on ct.ID = vw.EntityRecordID " +
                " where EntityID = 1527 " +
                " order by idx.[RANK] desc ";
            var textSearchResult = session.CreateSQLQuery(fullTextIndexQuery)
                .SetParameter("search", search)
                .List();

            // TOODO: try to work in a way of strongly typing a collection
            var resultingList = new List<AptifriedClassTextSearchResultDto>();
            foreach (object[] item in textSearchResult) {
                resultingList.Add(new AptifriedClassTextSearchResultDto() {
                    Id = Convert.ToInt32(item[0]),
                    Name = Convert.ToString(item[1]),
                    Rank = Convert.ToInt32(item[2])
                });
            }

            return resultingList;
        }
    }
}