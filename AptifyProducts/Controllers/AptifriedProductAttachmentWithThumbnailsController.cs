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
    public class AptifriedProductAttachmentWithThumbnailsController : AptifyEnabledApiController {
        public AptifriedProductAttachmentWithThumbnailsController(ISession session) : base(session) { }

        public IList<AptifriedProductAttachmentWithThumbnailDto> Get() {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedProductAttachmentWithThumbnail>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedProductAttachmentWithThumbnail>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedProductAttachmentWithThumbnail>();
            IList<AptifriedProductAttachmentWithThumbnailDto> attDto = 
                new List<AptifriedProductAttachmentWithThumbnailDto>();
            attDto = Mapper.Map(hibernatedCol, 
                new List<AptifriedProductAttachmentWithThumbnailDto>());
            return attDto;

        }

        public HttpResponseMessage Get(int productAttachmentWithThumbNailId, bool thumbNail) {
            try {
                byte[] content = null;

                if (productAttachmentWithThumbNailId <= 0)
                    throw new System.Web.HttpException("Could not find attachment to find.",
                        new ArgumentException("atachmentToRetrieve"));

                AptifriedProductAttachmentWithThumbnail attachmentProper = 
                    GetProductAttachmentWithThumbnail(productAttachmentWithThumbNailId);

                if (attachmentProper == null) {
                    throw new System.Web.HttpException(404, "Resource not found.", new ArgumentException("That does not exist."));
                }

                System.Collections.IList queryResult = null;
                if (thumbNail) {
                    queryResult = session.CreateSQLQuery("select ThumbnailBlob from vwProductAttachmentWithThumbnails where Id = :attachmentId")
                        .SetInt32("attachmentId", attachmentProper.Id)
                        .List();
                } else {
                    queryResult = session.CreateSQLQuery("select FileBlob from vwProductAttachmentWithThumbnails where Id = :attachmentId")
                        .SetInt32("attachmentId", attachmentProper.Id)
                        .List();
                }

                if (queryResult != null && queryResult.Count == 1) {
                    content = (byte[])queryResult[0];
                }

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(content);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                if (thumbNail) {
                    result.Content.Headers.ContentDisposition.FileName = attachmentProper.ThumbnailName;
                } else {
                    result.Content.Headers.ContentDisposition.FileName = attachmentProper.FileName;
                }
                return result;
            } catch (Exception ex) {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private AptifriedProductAttachmentWithThumbnail GetProductAttachmentWithThumbnail(int id) {
            var attachmentWithThumb = session.QueryOver<AptifriedProductAttachmentWithThumbnail>()
                .Where(x => x.Id == id)
                .Take(1)
                .SingleOrDefault();

            return attachmentWithThumb;
        }

    }
}