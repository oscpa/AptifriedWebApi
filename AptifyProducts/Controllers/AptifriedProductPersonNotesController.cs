using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers {
	[Authorize]
	public class AptifriedProductPersonNotesController : AptifyEnabledApiController {
		public AptifriedProductPersonNotesController(ISession session) : base(session) { }

		public IList<AptifriedProductPersonNoteDto> Get() {
			var queryString = Request.RequestUri.Query;
			ICriteria queryCriteria = session.CreateCriteria<AptifriedProductPersonNote>();
			try {
				if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
					queryString = queryString.Remove(0, 1);
				}
				queryCriteria = session.ODataQuery<AptifriedProductPersonNote>(queryString);
			} catch (ODataException odataException) {
				throw new HttpException(500, "Homie don't play that.", odataException);
			}
			var hibernatedCol = queryCriteria.List<AptifriedProductPersonNote>();
			IList<AptifriedProductPersonNoteDto> notesDto = new List<AptifriedProductPersonNoteDto>();
			notesDto = Mapper.Map(hibernatedCol, new List<AptifriedProductPersonNoteDto>());
			return notesDto;
		}

		public AptifriedProductPersonNoteDto Post(AptifriedProductPersonNoteDto note) {
			if (note == null) {
				throw new HttpException(500, "No note DTO provided!");
			}

			int newNoteId = -1;
			if (note.Id > 0) {
				newNoteId = note.Id;
			}

			var noteGe = AptifyApp.GetEntityObject("ProductPersonNotes", newNoteId);

			noteGe.SetValue("ProductID", note.Product.Id);
			noteGe.SetValue("PersonID", note.Person.Id);
			noteGe.SetValue("Body", note.Body);

			if (newNoteId == -1) {
				noteGe.SetValue("DateCreated", System.DateTime.Now);
			}
			noteGe.SetValue("DateUpdated", System.DateTime.Now);

			noteGe.Save(false);

			if (!string.IsNullOrEmpty(noteGe.LastUserError)) {
				throw new HttpException(500, noteGe.LastUserError);
			}

			// Set the ID that we just got
			note.Id = Convert.ToInt32(noteGe.RecordID);

			return note;
		}

		public bool Delete(int noteId) {
			if (noteId < 1) {
				throw new HttpException(500, "No note ID provided!");
			}

			AptifyGenericEntityBase noteGe = AptifyApp.GetEntityObject("ProductPersonNotes", noteId);
			if (noteGe != null) {
				noteGe.Delete();

				if (!string.IsNullOrEmpty(noteGe.LastUserError)) {
					throw new HttpException(500, noteGe.LastUserError);
				}

				return true;
			} else {
				throw new HttpException(500, "No note with that ID!");
			}
		}
	}
}