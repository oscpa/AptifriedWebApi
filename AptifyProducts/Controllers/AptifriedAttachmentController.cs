using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers {
    public class AptifriedAttachmentController : AptifyEnabledApiController {
        public AptifriedAttachmentController(ISession session) : base(session) { }

        public IEnumerable<AptifriedAttachmentDto> Get() {

            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedAttachment>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedAttachment>
                    (session, queryString);


            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedAttachment>();
            IList<AptifriedAttachmentDto> attachmentsDto = new List<AptifriedAttachmentDto>();
            attachmentsDto = Mapper.Map(hibernatedCol, new List<AptifriedAttachmentDto>());
            return attachmentsDto;
        }

        public IEnumerable<AptifriedAttachmentDto> Get(int parentProductId, bool recursive) {

            IList<AptifriedAttachment> attachments = null;
            if (recursive) {

                attachments = session.CreateSQLQuery(" select  at.ID , at.Name , at.Description , at.CategoryID , at.Category , at.EntityID , at.Entity , at.RecordID , at.LocalFileName , at.DateCreated , at.WhoCreated , at.DateUpdated , at.WhoUpdated , at.Status , at.CheckedOutByID , at.CheckedOutBy , at.DateCheckedOut , at.Compressed , at.BlobData , at.Comments " +
                    " from dbo.vwAttachments at  " +
                    " where at.EntityID = 926 " +
                    " and at.RecordId in (select * from dbo.fnOscpaGetChildMeetingsProductID(:parentProductId)) ")
                    .AddEntity("at", typeof(AptifriedAttachment))
                    .SetInt32("parentProductId", parentProductId)
                    .List<AptifriedAttachment>();

            } else {

                attachments = session.QueryOver<AptifriedAttachment>()
                    .Where(x => x.EntityId == 926)
                    .And(x => x.RecordId == parentProductId)
                    .List<AptifriedAttachment>();
            }

            IList<AptifriedAttachmentDto> resultingAttachments = new List<AptifriedAttachmentDto>();
            resultingAttachments = Mapper.Map(attachments, new List<AptifriedAttachmentDto>());
            return resultingAttachments;

        }

        public HttpResponseMessage Get(int attachmentId) {
            try {
                byte[] content = null;

                if (attachmentId <= 0)
                    throw new HttpException("Could not find attachment to find.", 
                        new ArgumentException("atachmentToRetrieve"));

                AptifriedAttachment attachmentProper = GetAttachment(attachmentId);

                // hack a query to get a quick byte array 
                var queryResult = session.CreateSQLQuery("select BlobData from vwAttachments where Id = :attachmentId")
                    .SetInt32("attachmentId", attachmentProper.Id)
                    .List();

                if (queryResult != null && queryResult.Count == 1) {
                    content = (byte[])queryResult[0];
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(content);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = attachmentProper.Name;
                return result;
            } catch (Exception ex) {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private AptifriedAttachment GetAttachment(int attachentId) {
            var attachment = session.QueryOver<AptifriedAttachment>()
                .Where(x => x.Id == attachentId)
                .Take(1)
                .SingleOrDefault();

            return attachment;
        }
    }
}