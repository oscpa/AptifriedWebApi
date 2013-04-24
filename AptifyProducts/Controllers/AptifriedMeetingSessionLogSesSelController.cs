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
using NHibernate.Criterion;

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

        public AptifriedMeetingSessionLogDto Post(AptifriedMeetingSessionLogDto sessionChanges) {

            if (sessionChanges == null)
                throw new HttpException(500, "Session changes was not included.");

            if ((sessionChanges.NewSessions != null && sessionChanges.NewSessions.Count == 0) &&
                (sessionChanges.CancelledSessions != null && sessionChanges.CancelledSessions.Count == 0))
                throw new HttpException(500, "No session changes to process.");

            // we'll assume that this code doesn't need to validate necessary steps to unregister

            var sessionChangeEntity = AptifyApp.GetEntityObject(SESSION_CHANGE_ENTITY, -1);
            sessionChangeEntity.SetValue("BillToID", AptifyUser.PersonId);
            //sessionChangeEntity.SetValue("BillToCompanyID", sessionChanges.BillToCompanyId);
            sessionChangeEntity.SetValue("ShipToID", AptifyUser.PersonId);
            //sessionChangeEntity.SetValue("ShipToCompanyID", sessionChanges.ShipToCompanyId);

            foreach (var cancellation in sessionChanges.CancelledSessions) {
                if (cancellation.ProductId > 0) {
                    var newCancellationEntity = (Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase)
                        sessionChangeEntity.SubTypes[SESSION_CHANGE_CANCELLATION_ENTITY].Add();
                    /*
                    newCancellationEntity.SetValue("OrderID", cancellation.OrderId);
                    newCancellationEntity.SetValue("OrderMeetingDetailID", cancellation.OrderMeetingDetailId);
                    */

                    var relevantMeetingDetail = GetMeetingDetailRecordForProdcut(cancellation.ProductId);
                    newCancellationEntity.SetValue("OrderID", relevantMeetingDetail.OrderId);
                    newCancellationEntity.SetValue("OrderMeetingDetailID", relevantMeetingDetail.Id);

                    newCancellationEntity.SetValue("ProductID", cancellation.ProductId);
                    newCancellationEntity.SetValue("StatusID", 4); // hard coded to "Cancelled"
                }
            }

            foreach (var addition in sessionChanges.NewSessions) {
                if (addition.ProductId > 0) {
                    var newAdditionEnitty = (Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase)
                        sessionChangeEntity.SubTypes[SESSION_CHANGE_ADDITION_ENTITY].Add();

                    newAdditionEnitty.SetValue("ProductID", addition.ProductId);
                    newAdditionEnitty.SetValue("AttendeeStatusID", addition.AttendeeStatusId);
                }
            }

            string errorMessage = string.Empty;
            if (!sessionChangeEntity.Save(false, ref errorMessage)) {
                throw new HttpException(500, string.Format("An error occurred trying to process session changes. (Error: {0}) ", errorMessage));
            }

            return sessionChanges;
        }


        /// <summary>
        /// In order to keep clear of maintinaing order information on the client side,
        /// this function should retrieve the last "not Cancelled" order and Order meeting
        /// Detail so that the pocess of generating a cancellation can continue.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private AptifriedMeetingDetail GetMeetingDetailRecordForProdcut(int productId) {
            AptifriedMeetingDetail resultingRegistration = null;

            var discoveredOrderMeetingDetail = session.QueryOver<AptifriedMeetingDetail>()
                .Where(x => x.Status.Id != 4)
                .Where(x => x.Product.Id == productId)
                .Where(x => x.Attendee.Id == AptifyUser.PersonId)
                .List<AptifriedMeetingDetail>();

            if (discoveredOrderMeetingDetail.Count == 1) {
                resultingRegistration = discoveredOrderMeetingDetail.First();
            } else if (discoveredOrderMeetingDetail.Count > 1) {
                throw new HttpException(500, "Conflict, found more than one registration for product: " + productId.ToString());
            } else if (discoveredOrderMeetingDetail.Count == 0) {
                throw new HttpException(500, "Could not find an existing registration for product: " + productId.ToString());
            }

            return resultingRegistration;
        }
        
    }
}