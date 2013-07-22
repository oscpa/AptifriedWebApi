using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Aptify.Framework.Application;
using Aptify.Framework.DataServices;
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
			} else {
				// Return the ID, as a courtesy
				cpeDto.Attachment.Id = Convert.ToInt32(entityObj.RecordID);
			}

			byte[] dataBytes = Convert.FromBase64String(cpeDto.Base64Data);

			if (dataBytes == null || dataBytes.Length < 1) {
				throw new HttpException(500, "Error decoding base 64 data to byte array");
			}

			string tempPath = null;
			try {
				tempPath = Path.Combine(new String[] { Path.GetTempPath(), cpeDto.Attachment.Name });
				File.WriteAllBytes(tempPath, dataBytes);

				var atts = new AptifyAttachments(AptifyApp, "Persons", cpeDto.Attachment.RecordId);

				atts.UpdateFile(Convert.ToInt32(entityObj.RecordID), tempPath);
			} catch (IOException ex) {
				throw new HttpException(500, "We got issues writing the temp files, yo", ex);
			} finally {
				if (tempPath != null && File.Exists(tempPath)) {
					File.Delete(tempPath);
				}
			}

			//// Let's do this with straight up aptify DA
			//var aptifyDA = new DataAction(AptifyApp.UserCredentials);
			//aptifyDA.ExecuteNonQueryParametrized("update Attachment set BlobData = @data where ID = @id",
			//	System.Data.CommandType.Text,
			//	new System.Data.SqlClient.SqlParameter[] {
			//		new System.Data.SqlClient.SqlParameter("@id", entityObj.RecordID),
			//		new System.Data.SqlClient.SqlParameter("@data", dataBytes)
			//	});

			return cpeDto.Attachment;
		}

		public bool Delete(int personId, int attachmentId) {
			var atts = new AptifyAttachments(AptifyApp, "Persons", personId);
			return atts.DeleteAttachment(attachmentId);
		}
	}
}