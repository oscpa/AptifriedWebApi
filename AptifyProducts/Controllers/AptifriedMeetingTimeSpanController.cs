#region using

using System.Collections.Generic;
using System.Web;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Models.Dto.Meeting;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedMeetingTimeSpanController : AptifyEnabledApiController
    {
        public AptifriedMeetingTimeSpanController(ISession session) : base(session)
        {
        }

        public IList<AptifriedMeetingTimeSpanDto> Get()
        {
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingTimeSpan>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedMeetingTimeSpan>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedMeetingTimeSpan>();
            IList<AptifriedMeetingTimeSpanDto> timeSlotsDto = new List<AptifriedMeetingTimeSpanDto>();
            timeSlotsDto = Mapper.Map(hibernatedCol, new List<AptifriedMeetingTimeSpanDto>());
            return timeSlotsDto;
        }
    }
}