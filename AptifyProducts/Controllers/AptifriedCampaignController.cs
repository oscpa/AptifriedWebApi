using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers {
	public class AptifriedCampaignController : AptifyEnabledApiController {
		public AptifriedCampaignController(ISession session) : base(session) { }

        public IList<AptifriedCampaignDto> Get() {

            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
			ICriteria queryCriteria = session.CreateCriteria<AptifriedCampaign>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
				queryCriteria = ODataParser.ODataQuery<AptifriedCampaign>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
			var hibernatedCol = queryCriteria.List<AptifriedCampaign>();
			IList<AptifriedCampaignDto> campaignDto = new List<AptifriedCampaignDto>();
			campaignDto = Mapper.Map(hibernatedCol, new List<AptifriedCampaignDto>());
            return campaignDto;
        }
	}
}