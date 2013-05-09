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
	public class AptifriedProductRelationController : AptifyEnabledApiController {
		public AptifriedProductRelationController(ISession session) : base(session) { }

        public IList<AptifriedProductRelationDto> Get() {

            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedProductRelation>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedProductRelation>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie definitely don't play that, bro", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedProductRelation>();
            IList<AptifriedProductRelationDto> relationDto = new List<AptifriedProductRelationDto>();
            relationDto = Mapper.Map(hibernatedCol, new List<AptifriedProductRelationDto>());
            return relationDto;
        }
	}
}