using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;

namespace AptifyWebApi.Controllers {
	public class AptifriedWebShoppingCartDetailsCountController : AptifyEnabledApiController {
		public AptifriedWebShoppingCartDetailsCountController(ISession session) : base(session) { }

		public IList<AptifriedWebShoppingCartDetailsCountDto> Get(int webUserId) {
			return GetByWebUserID(webUserId);
		}

		private IList<AptifriedWebShoppingCartDetailsCountDto> GetByWebUserID(int webUserId) {
			IList<AptifriedWebShoppingCartDetailsCount> hibernated =
				session.CreateSQLQuery("select * from vwStoreWebShoppingCartDetailsCount where WebUserID = :webUserId")
				.AddEntity("vwStoreWebShoppingCartDetailsCount", typeof(AptifriedWebShoppingCartDetailsCount))
				.SetInt32("webUserId", webUserId)
				.List<AptifriedWebShoppingCartDetailsCount>();

			return Mapper.Map(hibernated, new List<AptifriedWebShoppingCartDetailsCountDto>());
		}
	}
}