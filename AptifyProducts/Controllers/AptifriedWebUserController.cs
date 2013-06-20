using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using NHibernate;

namespace AptifyWebApi.Controllers {
	public class AptifriedWebUserController : AptifyEnabledApiController {
		public AptifriedWebUserController(ISession session) : base(session) { }

		public AptifriedWebUserDto Put(AptifriedWebUserDto webUserDto) {
			return createWebUser(webUserDto);
		}

		private AptifriedWebUserDto createWebUser(AptifriedWebUserDto webUserDto) {
			AptifyGenericEntityBase webUserGE = AptifyApp.GetEntityObject("Web Users", -1);
			if (webUserGE == null) {
				throw new HttpException(500, "Error creating new web user");
			}

			webUserGE.SetValue("UserID", webUserDto.UserName);
			webUserGE.SetValue("FirstName", webUserDto.FirstName);
			webUserGE.SetValue("LastName", webUserDto.LastName);
			webUserGE.SetValue("Email", webUserDto.Email);
			webUserGE.SetValue("Disabled", false);
			webUserGE.SetValue("LinkID", webUserDto.LinkId);

			if (!webUserGE.Save(false)) {
				throw new HttpException(500, "Error saving new web user: " + webUserGE.LastUserError);
			}

			return webUserDto;
		}
	}
}