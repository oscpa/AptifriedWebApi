using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
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

			if (userDto.LinkId == null || userDto.LinkId < 1) {
				throw new HttpException(500, "No link ID given for web user DTO.");
			}

			AptifriedWebUser webUser = session.QueryOver<AptifriedWebUser>().Where(x => x.LinkId == userDto.LinkId).List<AptifriedWebUser>().FirstOrDefault<AptifriedWebUser>();

			if (webUser == null) {
				throw new HttpException(500, "No web user found corresponding to the link id in the web user DTO.");
			}

			AptifyGenericEntityBase geWebUser = AptifyApp.GetEntityObject("Web Users", webUser.Id);
			if (geWebUser == null) {
				throw new HttpException(500, "Error getting Web user GE for the located web user.");
			}

			geWebUser.SetValue("PWD", userDto.Password);

			if (!geWebUser.Save(false)) {
				throw new HttpException(500, geWebUser.LastUserError);
			}

			return true;
		}
	}
}