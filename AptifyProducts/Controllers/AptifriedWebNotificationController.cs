using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers {
	public class AptifriedWebNotificationController : AptifyEnabledApiController {
		public AptifriedWebNotificationController(ISession session) : base(session) {}

		public IList<AptifriedWebNotificationDto> Get() {
			var queryString = Request.RequestUri.Query;
			ICriteria queryCriteria = session.CreateCriteria<AptifriedWebNotification>();
			try {
				if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
					queryString = queryString.Remove(0, 1);
				}
				queryCriteria = ODataParser.ODataQuery<AptifriedWebNotification>
					(session, queryString);
			} catch (NHibernate.OData.ODataException odataException) {
				throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
			}
			var hibernatedCol = queryCriteria.List<AptifriedWebNotification>();
			IList<AptifriedWebNotificationDto> dtoList = new List<AptifriedWebNotificationDto>();
			dtoList = Mapper.Map(hibernatedCol, new List<AptifriedWebNotificationDto>());
			return dtoList;
		}

		public void Post(AptifriedWebNotificationDto dto) {
			if (dto == null) {
				throw new HttpException(500, "No notification DTO or no DTO ID field, yo");
			}

			saveNotificationDto(dto, dto.Id);
		}

		private void saveNotificationDto(AptifriedWebNotificationDto dto, int id) {
			AptifyGenericEntityBase geNotification = AptifyApp.GetEntityObject("WebNotifications", id);

			if (id == -1) {
				geNotification.SetValue("DateCreated", DateTime.Now);
			}
			if (!string.IsNullOrEmpty(dto.Name)) {
				geNotification.SetValue("Name", dto.Name);
			}
			if (!string.IsNullOrEmpty(dto.Description)) {
				geNotification.SetValue("Description", dto.Description);
			}
			geNotification.SetValue("Seen", dto.Seen);
			if (dto.PersonId > 0) {
				geNotification.SetValue("PersonID", dto.PersonId);
			}

			if (!geNotification.Save(false)) {
				throw new HttpException(500, "Error saving entity: " + geNotification.LastUserError);
			}
		}
	}
}