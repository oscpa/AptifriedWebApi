using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingTimeSpanController : AptifyEnabledApiController {
        public AptifriedMeetingTimeSpanController(ISession session) : base(session) { }

        public IList<AptifriedMeetingTimeSpanDto> Get() {
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingTimeSpan>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedMeetingTimeSpan>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedMeetingTimeSpan>();
            IList<AptifriedMeetingTimeSpanDto> timeSlotsDto = new List<AptifriedMeetingTimeSpanDto>();
            timeSlotsDto = Mapper.Map(hibernatedCol, new List<AptifriedMeetingTimeSpanDto>());
            return timeSlotsDto;
        }
    }
}