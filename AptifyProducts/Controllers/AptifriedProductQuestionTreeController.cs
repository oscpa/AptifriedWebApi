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
	public class AptifriedProductQuestionTreeController : AptifyEnabledApiController {
		public AptifriedProductQuestionTreeController(ISession session) : base(session) { }

		public IList<AptifriedProductQuestionTreeDto> Get() {
			var queryString = Request.RequestUri.Query;
			ICriteria queryCriteria = session.CreateCriteria<AptifriedProductQuestionTree>();
			try {
				if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
					queryString = queryString.Remove(0, 1);
				}
				queryCriteria = ODataParser.ODataQuery<AptifriedProductQuestionTree>
					(session, queryString);
			} catch (NHibernate.OData.ODataException odataException) {
				throw new System.Web.HttpException(500, "Problem with AptifriedProductQuestionTreeController oData stuff", odataException);
			}

			return Mapper.Map(queryCriteria.List<AptifriedProductQuestionTree>(), new List<AptifriedProductQuestionTreeDto>());
		}
	}
}