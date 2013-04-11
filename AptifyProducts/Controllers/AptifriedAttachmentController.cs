﻿using AptifyWebApi.Dto;
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

        public HttpResponseMessage Post(AptifriedAttachmentDto attachmentToRetrieve) {
            try {
                byte[] content = null;

                if (attachmentToRetrieve == null)
                    throw new HttpException("Could not find attachment to find.", 
                        new ArgumentException("atachmentToRetrieve"));

                AptifriedAttachment attachmentProper = GetAttachment(attachmentToRetrieve);

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

        private AptifriedAttachment GetAttachment(AptifriedAttachmentDto attachmentDto) {
            var attachment = session.QueryOver<AptifriedAttachment>()
                .Where(x => x.Id == attachmentDto.Id)
                .Take(1)
                .SingleOrDefault();

            return attachment;
        }
    }
}