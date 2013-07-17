#region using

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Aptify.Applications.OrderEntry;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedCartSubmitController : AptifyEnabledApiController
    {
        public AptifriedCartSubmitController(ISession session) : base(session)
        {
        }

        public AptifriedOrderDto Post(AptifriedWebShoppingCartSubmitRequestDto submitRequest)
        {
            AptifriedOrderDto resultingOrder = null;
            AptifyGenericEntityBase orderGe = null;
            AptifyGenericEntityBase webShoppingCartGe = null;

            if (submitRequest == null)
                throw new HttpException(500, "Post must contain the submitRequest",
                                        new ArgumentException("submitRequest"));

            var existingCarts = GetCarts(submitRequest.SavedShoppingCartId);

            if (existingCarts.Count > 1)
                throw new HttpException(500, "More than one cart returned from the id... what?! How?!");

            if (existingCarts.Count == 0)
                throw new HttpException(500, "No carts returned");

            OrdersEntity orderProper = null;

            if (existingCarts[0].OrderId > 0)
            {
                orderProper = (OrdersEntity) AptifyApp.GetEntityObject("Orders", existingCarts[0].OrderId);
                if (orderProper.OrderStatus == OrderStatus.Shipped)
                    throw new HttpException(500, "This cart has already been submitted");
            }
            else
            {
                var cartController = new AptifriedCartController(session);
                AptifriedWebShoppingCartDto cartResult;
                cartController.GetOrderEntity(existingCarts[0], out cartResult, out orderProper);
            }

            if ((orderProper.GetValue("OrderDate", true) == null))
                orderProper.SetAddValue("OrderDate", DateTime.Now);

            orderProper.ShipToID = AptifyUser.PersonId;
            orderProper.BillToID = AptifyUser.PersonId;
            orderProper.EmployeeID = 1; //ebiz

            // if it's a free product, then we need to ignore payment.
            if (orderProper.GrandTotal > 0)
            {
                orderProper.SetAddValue("InitialPaymentAmount", orderProper.GrandTotal);
                orderProper.SetAddValue("PayTypeID", submitRequest.PaymentTypeId);
                orderProper.SetAddValue("CCAccountNumber", submitRequest.CardNumber);
                orderProper.SetAddValue("CCExpireDate", GetCreditCardExpirationDate(submitRequest));
                orderProper.SetAddValue("CCSecurityNumber", submitRequest.CardSvn);
                orderProper.SetAddValue("PaymentSource", submitRequest.PaymentSource);
            }

            orderProper.SetAddValue("OrderSourceID", 4); // hard coded for production/dev of "web" 
            orderProper.SetAddValue("OrderLevelID", 1);
            orderProper.SetAddValue("OrderLevel", "Regular");

            if (((AptifyGenericEntityBase) orderProper).Save(false))
            {
                webShoppingCartGe = AptifyApp.GetEntityObject("Web Shopping Carts", existingCarts[0].Id);

                if (webShoppingCartGe.RecordID != existingCarts[0].Id)
                    throw new HttpException("Could not load shopping cart from Aptify.");

                webShoppingCartGe.SetAddValue("OrderID", orderProper.ID);

                if (!webShoppingCartGe.Save(false))
                    throw new HttpException(500, "Could not associate saved cart with newly generated order.");

                if (orderProper.get_AvailableForShipping(true) == OrdersEntity.AutoShippingAvailabilityTypes.FullOrder)
                {
                    if (orderProper.ShipEntireOrder(false))
                    {
                        resultingOrder = Mapper.Map(orderProper, new AptifriedOrderDto());
                    }
                    else
                    {
                        throw new HttpException(500,
                                                "Could not ship order. Exception on order shipment wrapped in this exception.",
                                                new ApplicationException(orderProper.LastError));
                    }
                }
                else
                {
                    throw new HttpException(500, "Full Order not available for shipment.");
                }
            }
            else
            {
                throw new HttpException(500, "Could not save new before shipment. Error is: " + orderProper.LastError);
            }

            return resultingOrder;
        }


        private static DateTime GetCreditCardExpirationDate(AptifriedWebShoppingCartSubmitRequestDto submitRequest)
        {
            return new DateTime(
                submitRequest.CardExpirationYear,
                submitRequest.CardExpirationMonth,
                DateTime.DaysInMonth(
                    submitRequest.CardExpirationYear,
                    submitRequest.CardExpirationMonth));
        }

        private IList<AptifriedWebShoppingCart> GetCarts(int shoppingCartId)
        {
            var shoppingCarts =
                session.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.ID = " + shoppingCartId.ToString() + " and users.UserID = '" + User.Identity.Name +
                    "'")
                       .AddEntity("carts", typeof (AptifriedWebShoppingCart))
                       .List<AptifriedWebShoppingCart>();

            return shoppingCarts;
        }
    }
}