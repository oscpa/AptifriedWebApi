using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;

namespace AptifyWebApi.Controllers {
	public class AptifriedHealthCheckController : AptifyEnabledApiController {
		public AptifriedHealthCheckController(ISession session) : base(session) { }

		public AptifriedPersonDto Get() {
			AptifriedPersonDto personDto = null;

			personDto = Mapper.Map<AptifriedPerson, AptifriedPersonDto>(session.QueryOver<AptifriedPerson>().Where(x => x.Id == 31515).SingleOrDefault());

			return personDto;
		}
	}
}