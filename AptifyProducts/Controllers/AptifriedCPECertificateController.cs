using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using NHibernate;

namespace AptifyWebApi.Controllers {
	[System.Web.Http.Authorize]
	public class AptifriedCPECertificateController : AptifyEnabledApiController {
		private const int PERSONS_ENTITY_ID = 1006;

		public AptifriedCPECertificateController(ISession session) : base(session) { }

		public AptifriedAttachmentDto Post(AptifriedCPECertificateDto cpeDto) {
			if (cpeDto == null || cpeDto.Base64Data == null || cpeDto.Base64Data.Length < 1 || string.IsNullOrEmpty(cpeDto.Attachment.Name)) {
				throw new HttpException(500, "No file data to upload or no file name given");
			}

			if (cpeDto.Attachment.EntityId != PERSONS_ENTITY_ID || cpeDto.Attachment.RecordId < 1) {
				throw new HttpException(500, "Attempt to insert attachment with invalid entity/record values");
			}

			var entityObj = AptifyApp.GetEntityObject("Attachments", -1);
			entityObj.SetValue("Name", cpeDto.Attachment.Name);
			entityObj.SetValue("CategoryID", cpeDto.Attachment.Category.Id);
			entityObj.SetValue("EntityID", PERSONS_ENTITY_ID);
			entityObj.SetValue("RecordID", cpeDto.Attachment.RecordId);
			entityObj.SetValue("DateCreated", cpeDto.Attachment.DateCreated);
			entityObj.SetValue("WhoCreated", "Aptify_EBiz");
			entityObj.SetValue("Status", cpeDto.Attachment.Status);

			if (!entityObj.Save(false)) {
				throw new HttpException(500, "Dat is wack: " + entityObj.LastUserError);
			}

			byte[] dataBytes = Convert.FromBase64String(cpeDto.Base64Data);

			if (dataBytes == null || dataBytes.Length < 1) {
				throw new HttpException(500, "Error decoding base 64 data to byte array");
			}

			using (var transaction = session.BeginTransaction())
			{
				//WBN: Refactor this out to a base/CpeCert repo
				try
				{
					var q = session.QueryOver<AptifriedAttachment>().Where(x => x.Id == entityObj.RecordID).SingleOrDefault<AptifriedAttachment>();

					q.BlobData = dataBytes;
				   
					transaction.Commit();
				}
				catch (Exception ex)
				{
					var e = ex.ToString();

					transaction.Rollback();

					throw new HttpException(500, "Warning: no entities updated when inserting blob into attachments entity");
				}
			}

			/*
			var query = session.CreateSQLQuery("update vwAttachments set BlobData = :data where ID = :id")
				.SetInt64("id", entityObj.RecordID)
				.SetBinary("data", dataBytes);

			if (query.ExecuteUpdate() < 1) {
				throw new HttpException(500, "Warning: no entities updated when inserting blob into attachments entity");
			}
			 */

			return cpeDto.Attachment;
		}
	}
}