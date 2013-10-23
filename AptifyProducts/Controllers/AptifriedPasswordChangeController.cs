using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using NHibernate;

namespace AptifyWebApi.Controllers {
	[Authorize]
	public class AptifriedPasswordChangeController : AptifyEnabledApiController {
		public AptifriedPasswordChangeController(ISession session) : base(session) { }

		public bool Post(AptifriedWebUserDto userDto) {
			if (userDto == null) {
				throw new HttpException(500, "No web user DTO provided.");
			}

			if (String.IsNullOrEmpty(userDto.Password)) {
				throw new HttpException(500, "No password given for web user DTO.");
			}



			return true;
		}
	}
}