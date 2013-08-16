#region using

using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedExamController : AptifyEnabledApiController
    {
        public AptifriedExamController(ISession session) : base(session)
        {
        }

        public IList<AptifriedExamDto> Get()
        {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedExam>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedExam>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedExam>();
            IList<AptifriedExamDto> examDto = new List<AptifriedExamDto>();
            examDto = Mapper.Map(hibernatedCol, new List<AptifriedExamDto>());
            return examDto;
        }
    }
}