#region using

using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedMeetingExternalWebMediaContentController : AptifyEnabledApiController
    {
        public AptifriedMeetingExternalWebMediaContentController(ISession session)
            : base(session)
        {
        }

        public IList<AptifriedMeetingExternalWebMediaContentDto> Get()
        {
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingExternalWebMediaContent>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedMeetingExternalWebMediaContent>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }

            var resultingMedia = queryCriteria.List<AptifriedMeetingExternalWebMediaContent>();
            var content = Mapper.Map(resultingMedia, new List<AptifriedMeetingExternalWebMediaContentDto>());
            return content;
        }
    }
}