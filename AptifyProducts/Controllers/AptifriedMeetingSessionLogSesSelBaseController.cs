#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Dto.SessionSelection;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    /// <summary>
    /// Abstracts the code to manage session selection logic so we can inherit from
    /// this and consume it's resources higher up the stack
    /// </summary>
    public class AptifriedMeetingSessionLogSesSelBaseController : AptifyEnabledApiController
    {
        private const string SESSION_CHANGE_ENTITY = "OSCPAMeetingSessionLog";
        private const string SESSION_CHANGE_CANCELLATION_ENTITY = "OSCPACancelledMeetingSessions";
        private const string SESSION_CHANGE_ADDITION_ENTITY = "OSCPANewMeetingSessions";

        public AptifriedMeetingSessionLogSesSelBaseController(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Adds all of the cancellations and registrations from our DTO into an aptify Generic Entity
        /// </summary>
        /// <param name="sessionChanges"></param>
        /// <returns></returns>
        internal AptifyGenericEntityBase
            AddRegistrationsAndCancellationsToMeetingSessionLog(AptifriedMeetingSessionLogDto sessionChanges)
        {
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

            foreach (var cancellation in sessionChanges.CancelledSessions)
            {
                if (cancellation.ProductId > 0)
                {
                    var newCancellationEntity = sessionChangeEntity.SubTypes[SESSION_CHANGE_CANCELLATION_ENTITY].Add();
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

            foreach (var addition in sessionChanges.NewSessions)
            {
                if (addition.ProductId > 0)
                {
                    var newAdditionEnitty = sessionChangeEntity.SubTypes[SESSION_CHANGE_ADDITION_ENTITY].Add();

                    newAdditionEnitty.SetValue("ProductID", addition.ProductId);
                    newAdditionEnitty.SetValue("AttendeeStatusID", addition.AttendeeStatusId);
                }
            }
            return sessionChangeEntity;
        }


        /// <summary>
        /// Fires code that was written to determine if there are any conflicts in the 
        /// sessions that are selected.
        /// </summary>
        /// <param name="sessionChangeEntity"></param>
        /// <returns></returns>
        internal IList<AptifriedMeetingDto> FindAnyConflictsInSessionChanges(
            AptifyGenericEntityBase sessionChangeEntity)
        {
            IList<AptifriedMeetingDto> resultingMeetingsInConflict = null;

            var conflictsFound = new DataTable();
            var sessionLogEntityProper =
                (OSCPAMeetingSessionLogEntity.OSCPAMeetingSessionLogEntity) sessionChangeEntity;

            if (sessionLogEntityProper.CheckForConflict(
                (AptifyGenericEntity) sessionChangeEntity, ref conflictsFound))
            {
                var conflictingMeetings = new List<AptifriedMeeting>();

                foreach (DataRow r in conflictsFound.Rows)
                {
                    var thisMeeting = session.QueryOver<AptifriedMeeting>()
                                             .Where(m => m.Product.Id == Convert.ToInt32(r["ID"]))
                                             .SingleOrDefault();

                    conflictingMeetings.Add(thisMeeting);
                }

                resultingMeetingsInConflict = Mapper.Map(conflictingMeetings, new List<AptifriedMeetingDto>());
            }

            return resultingMeetingsInConflict;
        }


        /// <summary>
        /// In order to keep clear of maintinaing order information on the client side,
        /// this function should retrieve the last "not Cancelled" order and Order meeting
        /// Detail so that the pocess of generating a cancellation can continue.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private AptifriedMeetingDetail GetMeetingDetailRecordForProdcut(int productId)
        {
            AptifriedMeetingDetail resultingRegistration = null;

            var discoveredOrderMeetingDetail = session.QueryOver<AptifriedMeetingDetail>()
                                                      .Where(x => x.Status.Id != 4)
                                                      .Where(x => x.Product.Id == productId)
                                                      .Where(x => x.Attendee.Id == AptifyUser.PersonId)
                                                      .List<AptifriedMeetingDetail>();

            if (discoveredOrderMeetingDetail.Count == 1)
            {
                resultingRegistration = discoveredOrderMeetingDetail.First();
            }
            else if (discoveredOrderMeetingDetail.Count > 1)
            {
                throw new HttpException(500,
                                        "Conflict, found more than one registration for product: " +
                                        productId.ToString());
            }
            else if (discoveredOrderMeetingDetail.Count == 0)
            {
                throw new HttpException(500,
                                        "Could not find an existing registration for product: " + productId.ToString());
            }

            return resultingRegistration;
        }
    }
}