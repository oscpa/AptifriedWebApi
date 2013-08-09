﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers {
	[Authorize]
	public class AptifriedEducationUnitAttachmentController : AptifyEnabledApiController {
		public AptifriedEducationUnitAttachmentController(ISession session) : base(session) {}

		public IList<AptifriedEducationUnitAttachmentDto> Get() {

			var queryString = Request.RequestUri.Query;
			ICriteria queryCriteria = session.CreateCriteria<AptifriedEducationUnitAttachment>();
			try {
				if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
					queryString = queryString.Remove(0, 1);
				}
				queryCriteria = ODataParser.ODataQuery<AptifriedEducationUnitAttachment>
					(session, queryString);

			} catch (NHibernate.OData.ODataException odataException) {
				throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
			}

			var models = queryCriteria.List<AptifriedEducationUnitAttachment>();
			IList<AptifriedEducationUnitAttachmentDto> dtos = new List<AptifriedEducationUnitAttachmentDto>();
			dtos = Mapper.Map(models, new List<AptifriedEducationUnitAttachmentDto>());

			return dtos;
		}

		public IList<AptifriedEducationUnitAttachmentDto> Get(long id) {
			var query = "select eu.* from vwEducationUnitAttachments eu where ID = :id";

			var models = session.CreateSQLQuery(query)
				.SetInt64("id", id)
				.SetEntity("eu", typeof(AptifriedEducationUnitAttachment))
				.List<AptifriedEducationUnitAttachment>();

			return Mapper.Map(models, new List<AptifriedEducationUnitAttachmentDto>());
		}

		public AptifriedEducationUnitAttachmentDto Post(AptifriedEducationUnitAttachmentDto dto) {
			if (dto == null) {
				throw new HttpException(500, "Gotta have a dto");
			}

			// Allow update
			long id = -1;
			if (dto.Id > 0) {
				id = dto.Id;
			}

			var ge = AptifyApp.GetEntityObject("EducationUnitAttachment", id);
			ge.SetValue("EducationUnitID", dto.EducationUnitId);
			ge.SetValue("AttachmentID", dto.AttachmentId);

			if (!ge.Save(false)) {
				throw new HttpException(500, "Error saving education unit attachment: " + ge.LastUserError);
			}

			return dto;
		}

		public bool Delete(long educationUnitAttachmentId) {
			if (educationUnitAttachmentId < 1) {
				throw new HttpException(500, "No ID given");
			}

			var educationUnitAttachmentEntity = AptifyApp.GetEntityObject("EducationUnitAttachments", educationUnitAttachmentId);
			if (educationUnitAttachmentEntity == null) {
				throw new HttpException(500, "Error loading up Education Units Attachment GE");
			}

			if (!educationUnitAttachmentEntity.Delete()) {
				throw new HttpException(500, "Error deleting entity: " + educationUnitAttachmentEntity.LastUserError);
			}

			return true;
		}
	}
}