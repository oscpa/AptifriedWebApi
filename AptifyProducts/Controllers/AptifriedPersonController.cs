using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers
{
	[System.Web.Http.Authorize]
    public class AptifriedPersonController : ApiController
    {
		private ISession session;

		public AptifriedPersonController(ISession session) {
			this.session = session;
		}

		public IEnumerable<AptifriedPersonDto> Get() {
			ICriteria criteria;
			try {
				String query = Request.RequestUri.Query;

				if (!string.IsNullOrEmpty(query) && query.Substring(0, 1) == @"?") {
					query = query.Remove(0, 1);
				}

				criteria = ODataParser.ODataQuery<AptifriedPerson>(session, query);
			} catch (ODataException exception) {
				throw new HttpException(500, "Ain't gonna fly", exception);
			}

			IList<AptifriedPerson> hibernatedDtos = criteria.List<AptifriedPerson>();
			IList<AptifriedPersonDto> personDtos = Mapper.Map(hibernatedDtos, new List<AptifriedPersonDto>());
			return personDtos;
		}

		public AptifriedPersonDto Get(Int32 personId) {
			var foundUser = session.QueryOver<AptifriedPerson>()
				.Where(x => x.Id == personId)
				.SingleOrDefault();

			if (foundUser != null) {
				return Mapper.Map(foundUser, new AptifriedPersonDto());
			}

			// Be explicit in failing
			return null;
		}
    }
}
