using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models;
using NHibernate;

namespace AptifyWebApi.Controllers {
	[Authorize]
	public class AptifriedCPECertificateController : AptifyEnabledApiController {
		private const int PERSONS_ENTITY_ID = 1006;

		public AptifriedCPECertificateController(ISession session) : base(session) { }

        [HttpDelete]
	    public bool Delete(int attachmentId)
	    {
	        using (var transaction = session.BeginTransaction())
	        {
	            try
	            {
	                var d =
	                    session.QueryOver<AptifriedAttachment>()
	                           .Where(x => x.Id == attachmentId)
	                           .SingleOrDefault<AptifriedAttachment>();

	                //session.Delete(d);

	                session.Flush();

	                transaction.Commit();

	                return true;
	            }
	            catch (Exception ex)
	            {
	                transaction.Rollback();

	                throw new HttpException(500, "Warning: no entities updated when inserting blob into attachments entity", ex.InnerException);
	            }
	        }
	    }

		[HttpPost]
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

				//WBN: Refactor this out to a base/CpeCert repo

		    using (var transaction = session.BeginTransaction())
		    {
		        try
		        {
		            var q =
		                session.QueryOver<AptifriedAttachment>()
		                       .Where(x => x.Id == entityObj.RecordID)
		                       .SingleOrDefault<AptifriedAttachment>();

		            q.BlobData = dataBytes;

		            session.Save(q);
		        }
		        catch (Exception ex)
		        {
		            var e = ex.ToString();

		            throw new HttpException(500, "Warning: no entities updated when inserting blob into attachments entity");
		        }
		    }
		    //InsertDataIntoDatabaseDirectlyDueToHibernateBug(entityObj.RecordID, dataBytes);

			return cpeDto.Attachment;
		}

	    private static void InsertDataIntoDatabaseDirectlyDueToHibernateBug(long recordId, IEnumerable dataBytes)
	    {

            const string sqlUpdate = "update vwAttachments set BlobData = :data where ID = :id";

            var conn = new SqlConnection(WebConfigHelper.DefaultConnectionString);
           conn.Open();
            try
            {
                var cmd = new SqlCommand(sqlUpdate, conn);
                cmd.Parameters.AddWithValue("@id", recordId);
                cmd.Parameters.AddWithValue("@data", dataBytes);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Warning: no entities updated when inserting blob into attachments entity");
            }
            finally
            {
                conn.Close();
            }
	    }
	}
}