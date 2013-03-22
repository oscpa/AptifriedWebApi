using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingController : AptifyEnabledApiController {

        public AptifriedMeetingController(ISession session) : base(session) { }

        public IList<AptifriedMeetingDto> Get() {

            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeeting>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedMeeting>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            } 
            var hibernatedCol = queryCriteria.List<AptifriedMeeting>();
            IList<AptifriedMeetingDto> meetingsDto = new List<AptifriedMeetingDto>();
            meetingsDto = Mapper.Map(hibernatedCol, new List<AptifriedMeetingDto>());
            return meetingsDto;
        }

        private IList<AptifriedMeetingDto> GetMeetings(int? meetingId) {
            List<AptifriedMeetingDto> resultingMeetings = new List<AptifriedMeetingDto>();
            if (meetingId.HasValue) {
                var meetingsReturned = session.QueryOver<AptifriedMeeting>()
                    .Where(x => x.Id == meetingId && x.StartDate >= DateTime.Now)
                    .List();

                resultingMeetings = Mapper.Map(meetingsReturned, new List<AptifriedMeetingDto>());
            } else {
                var allMeetings = session.QueryOver<AptifriedMeeting>()
                    .Where(x => x.StartDate >= DateTime.Now)
                    .List();

                resultingMeetings = Mapper.Map(allMeetings, new List<AptifriedMeetingDto>());
            }


            return resultingMeetings;
        }
    }
}