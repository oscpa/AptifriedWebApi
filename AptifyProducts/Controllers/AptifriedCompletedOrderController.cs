#region using

using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedCompletedOrderController : AptifyEnabledApiController
    {
        public AptifriedCompletedOrderController(ISession session) : base(session)
        {
        }

        public IEnumerable<AptifriedCompletedOrderDto> Get()
        {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedOrder>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedOrder>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }

            // only allow orders that are associated with the requesting user.
            queryCriteria.Add(Restrictions.Eq("ShipToPerson.Id", AptifyUser.PersonId));


            var hibernatedCol = queryCriteria.List<AptifriedOrder>();

            IList<AptifriedCompletedOrderDto> completedOrderDto = new List<AptifriedCompletedOrderDto>();
            completedOrderDto = Mapper.Map(hibernatedCol, new List<AptifriedCompletedOrderDto>());
            return completedOrderDto;
        }
    }
}