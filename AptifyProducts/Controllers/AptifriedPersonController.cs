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

namespace AptifyWebApi.Controllers
{
    public class AptifriedPersonController : ApiController
    {
		private ISession session;

		public AptifriedPersonController(ISession session) {
			this.session = session;
		}

		public AptifriedPersonDto Get() {
			return null;
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
