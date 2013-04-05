using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedMeetingExternalWebMediaContentController : AptifyEnabledApiController {
        
        public AptifriedMeetingExternalWebMediaContentController(ISession session)
            : base(session) {
        }

        public IList<AptifriedMeetingExternalWebMediaContentDto> Get() {
            IList<AptifriedMeetingExternalWebMediaContentDto> mediaContent = null;

            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingExternalWebMediaContent>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedMeetingExternalWebMediaContent>
                    (session, queryString);

            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }

            var resultingMedia = queryCriteria.List<AptifriedMeetingExternalWebMediaContent>();
            var content = Mapper.Map(resultingMedia, new List<AptifriedMeetingExternalWebMediaContentDto>());
            return content;
        }
    }
}