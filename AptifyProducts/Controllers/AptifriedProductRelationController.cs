#region using

using System.Collections.Generic;
using System.Web;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Aptifried;
using AptifyWebApi.Models.Dto;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedProductRelationController : AptifyEnabledApiController
    {
        public AptifriedProductRelationController(ISession session) : base(session)
        {
        }

        public IList<AptifriedProductRelationDto> Get()
        {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedProductRelation>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedProductRelation>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie definitely don't play that, bro", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedProductRelation>();
            IList<AptifriedProductRelationDto> relationDto = new List<AptifriedProductRelationDto>();
            relationDto = Mapper.Map(hibernatedCol, new List<AptifriedProductRelationDto>());
            return relationDto;
        }
    }
}