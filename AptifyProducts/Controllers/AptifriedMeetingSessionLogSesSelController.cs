using AptifyWebApi.Dto.SessionSelection;
using AptifyWebApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using AptifyWebApi.Models;
using AptifyWebApi.Models.SessionSelection;
using NHibernate.OData;
using AutoMapper;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedMeetingSessionLogSesSelController : AptifyEnabledApiController {
        private const string SESSION_CHANGE_ENTITY = "OSCPAMeetingSessionLog";
        private const string SESSION_CHANGE_CANCELLATION_ENTITY = "OSCPACancelledMeetingSessions";
        private const string SESSION_CHANGE_ADDITION_ENTITY = "OSCPANewMeetingSessions";

        public AptifriedMeetingSessionLogSesSelController(ISession session) : base(session) { }

        public IList<AptifriedMeetingSessionLogDto> Get() {
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingSessionLog>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedMeetingSessionLog>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedMeetingSessionLog>();
            return Mapper.Map(hibernatedCol, new List<AptifriedMeetingSessionLogDto>());

        }

        public bool Post(AptifriedMeetingSessionLogDto sessionChanges) {

            if (sessionChanges == null)
                throw new HttpException(500, "Session changes was not included.");

            if (sessionChanges.NewSessions == null || sessionChanges.NewSessions.Count == 0)
                throw new HttpException(500, "No session changes to process.");

            // we'll assume that this code doesn't need to validate necessary steps to unregister

            var sessionChangeEntity = AptifyApp.GetEntityObject(SESSION_CHANGE_ENTITY, -1);
            sessionChangeEntity.SetValue("BillToID", sessionChanges.BillToId);
            sessionChangeEntity.SetValue("BillToCompanyID", sessionChanges.BillToCompanyId);
            sessionChangeEntity.SetValue("ShipToID", sessionChanges.ShipToId);
            sessionChangeEntity.SetValue("ShipToCompanyID", sessionChanges.ShipToCompanyId);

            foreach (var cancellation in sessionChanges.CancelledSessions) {
                var newCancellationEntity = (Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase)
                    sessionChangeEntity.SubTypes[SESSION_CHANGE_CANCELLATION_ENTITY].Add();

                newCancellationEntity.SetValue("OrderID", cancellation.OrderId);
                newCancellationEntity.SetValue("OrderMeetingDetailID", cancellation.OrderMeetingDetailId);
                newCancellationEntity.SetValue("ProductID", cancellation.ProductId);
                newCancellationEntity.SetValue("StatusID", cancellation.StatusId);
                newCancellationEntity.Save(false);
            }

            foreach (var addition in sessionChanges.NewSessions) {
                var newAdditionEnitty = (Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase)
                    sessionChangeEntity.SubTypes[SESSION_CHANGE_ADDITION_ENTITY].Add();

                newAdditionEnitty.SetValue("ProductID", addition.ProductId);
                newAdditionEnitty.SetValue("AttendeeStatusID", addition.AttendeeStatusId);
            }

            return sessionChangeEntity.Save(false);
        }
    }
}