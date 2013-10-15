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
	public class AptifriedQuestionTreeController : AptifyEnabledApiController {
		public AptifriedQuestionTreeController(ISession session) : base(session) { }

		public IList<AptifriedQuestionTreeDto> Get() {
			var queryString = Request.RequestUri.Query;

			ICriteria queryCriteria = session.CreateCriteria<AptifriedQuestionTree>();

			try {
				if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
					queryString = queryString.Remove(0, 1);
				}
				queryCriteria = session.ODataQuery<AptifriedQuestionTree>(queryString);
			} catch (ODataException odataException) {
				throw new HttpException(500, "Homie don't play that.", odataException);
			}

			return Mapper.Map(queryCriteria.List<AptifriedQuestionTree>(), new List<AptifriedQuestionTreeDto>());
		}

		public IList<AptifriedQuestionTree> GetById(int id) {
			return session.QueryOver<AptifriedQuestionTree>()
				.Where(x => x.Id.Equals(id))
				.List<AptifriedQuestionTree>();
		}
	}
}