#region using

using System.Collections.Generic;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedCampaignController : AptifyEnabledApiController
    {
        public AptifriedCampaignController(ISession session) : base(session)
        {
        }

        public IList<AptifriedCampaignDto> Get()
        {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedCampaign>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedCampaign>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedCampaign>();
            IList<AptifriedCampaignDto> campaignDto = new List<AptifriedCampaignDto>();
            campaignDto = Mapper.Map(hibernatedCol, new List<AptifriedCampaignDto>());
            return campaignDto;
        }
    }
}