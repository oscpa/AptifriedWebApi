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
	public class AptifriedCartTransferController : AptifyEnabledApiController {
		public AptifriedCartTransferController(ISession session) : base(session) { }

		public AptifriedOrderDto Post(int cartId, int recipientId) {
			return InternalPost(cartId, recipientId);
		}

		private AptifriedOrderDto InternalPost(int cartId, int recipientId) {
			if (cartId > -1 && recipientId > -1) {
				AptifyGenericEntityBase cart = AptifyApp.GetEntityObject("Web Shopping Carts", cartId);
				if (cart != null) {
					var users =
					session.CreateSQLQuery(
						"select users.* from vwWebUsers users " +
						" where users.LinkID = '" + recipientId.ToString() + "'")
						.AddEntity("users", typeof(AptifriedWebUser))
						.List<AptifriedWebUser>();

					AptifriedWebUser theUser = users.FirstOrDefault();
					if (theUser != null) {
						cart.SetValue("WebUserID", theUser.Id);

						if (!cart.Save(false)) {
							throw new HttpException(500, "Couldn't save with the new user", new Exception(cart.LastUserError));
						} else {
							return null;
						}
					} else {
						throw new HttpException(500, "Err... couldn't get an object from that. Come again?");
					}
				} else {
					throw new HttpException(500, "Bad cart id, dude.");
				}
			} else {
				throw new HttpException(500, "Uh oh, someone done goofed -- y'all need both IDs here");
			}
		}
	}
}