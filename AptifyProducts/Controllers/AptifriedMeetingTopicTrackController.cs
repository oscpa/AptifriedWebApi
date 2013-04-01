using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using NHibernate.OData;
using AutoMapper;


namespace AptifyWebApi.Controllers {
    public class AptifriedMeetingTopicTrackController : AptifyEnabledApiController {
        public AptifriedMeetingTopicTrackController(ISession session) : base(session) { }

        public IList<AptifriedMeetingTopicTrackDto> Get() {

            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingTopicTrack>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedMeetingTopicTrack>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedMeetingTopicTrack>();
            IList<AptifriedMeetingTopicTrackDto> tracksDto = new List<AptifriedMeetingTopicTrackDto>();
            tracksDto = Mapper.Map(hibernatedCol, new List<AptifriedMeetingTopicTrackDto>());
            return tracksDto;
        }
    }
}