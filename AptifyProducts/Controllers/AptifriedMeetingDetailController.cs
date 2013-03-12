﻿using System;
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

namespace AptifyWebApi.Controllers {
	[System.Web.Http.Authorize]
	public class AptifriedMeetingDetailController : ApiController {
		private ISession session;

		public AptifriedMeetingDetailController(ISession session) {
			this.session = session;
		}

		public IEnumerable<AptifriedMeetingDetailDto> Get() {
			ICriteria criteria;
			try {
				String query = Request.RequestUri.Query;

				if (!string.IsNullOrEmpty(query) && query.Substring(0, 1) == @"?") {
					query = query.Remove(0, 1);
				}

				criteria = ODataParser.ODataQuery<AptifriedMeetingDetail>(session, query);
			} catch (ODataException exception) {
				throw new HttpException(500, "No sir", exception);
			}

			IList<AptifriedMeetingDetail> hibernatedDtos = criteria.List<AptifriedMeetingDetail>();
			IList<AptifriedMeetingDetailDto> meetingDtos = Mapper.Map(hibernatedDtos, new List<AptifriedMeetingDetailDto>());
			return meetingDtos;
		}
	}
}