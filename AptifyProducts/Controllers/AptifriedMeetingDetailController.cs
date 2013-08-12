#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;
using NHibernate.Linq;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedMeetingDetailController : ApiController
    {
        private readonly ISession session;

        public AptifriedMeetingDetailController(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<AptifriedMeetingDetailDto> Get()
        {
            ICriteria criteria;
            try
            {
                String query = Request.RequestUri.Query;

                if (!string.IsNullOrEmpty(query) && query.Substring(0, 1) == @"?")
                {
                    query = query.Remove(0, 1);
                
                }

                query = HttpUtility.UrlDecode(query);

                criteria = session.ODataQuery<AptifriedMeetingDetail>(query);
            }
            catch (ODataException exception)
            {
                throw new HttpException(500, "No sir", exception);
            }

            IList<AptifriedMeetingDetail> hibernatedDtos = criteria.List<AptifriedMeetingDetail>();
            
            
            IList<AptifriedMeetingDetailDto> meetingDtos = Mapper.Map(hibernatedDtos,
                                                                      new List<AptifriedMeetingDetailDto>());
            return meetingDtos;
        }
    }
}