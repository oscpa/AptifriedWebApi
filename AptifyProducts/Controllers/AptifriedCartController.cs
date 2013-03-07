using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Repository;
using AptifyWebApi.Attributes;
using NHibernate;
using System.Web.Mvc;
using System.Web.Http;
using AptifyWebApi.Models;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Factories;
using Aptify.Applications.OrderEntry;

namespace AptifyWebApi.Controllers {

    [System.Web.Http.Authorize]
    public class AptifriedCartController : AptifyEnabledApiController {

        private const string WEB_SHOPPING_CART_ENTITY_NAME = "Web Shopping Carts";
        private const string WEB_SHOPPING_CART_DETAILS_ENTITY_NAME = "Web Shopping Cart Details";
        private const string ORDERS_ENTITY_NAME = "Orders";


        public AptifriedCartController(ISession session) : base(session) { }

        public IEnumerable<AptifriedWebShoppingCartDto> Get(int cartId) {
            return GetSavedCarts(cartId);
        }
        
        public IEnumerable<AptifriedWebShoppingCartDto> Get() {
            return GetSavedCarts(null);
        }

        public AptifriedWebShoppingCartDto Post(AptifriedWebShoppingCartRequestDto addRequest) {

            if (addRequest == null)
                throw new HttpException(500, "Add Request not present.", new ArgumentException("addRequest"));

            return SaveRequestedShoppingCart(addRequest);

        }

        public bool Delete(int cartId) {
            var myCarts = GetSavedShoppingCarts(cartId);
            bool successInDeletion = false;

            if (myCarts != null && myCarts.Count == 1) {

                var cartDetails = GetCartDetails(cartId);
                foreach (var cartDetail in cartDetails) {
                    var detailLineToDelete = AptifyApp.GetEntityObject(
                        WEB_SHOPPING_CART_DETAILS_ENTITY_NAME, 
                        cartDetail.Id);
                    successInDeletion = detailLineToDelete.Delete();
                }

                var cartToDelete = AptifyApp.GetEntityObject("Web Shopping Carts", Convert.ToInt64(cartId));    
                successInDeletion = cartToDelete.Delete();
            }

            return successInDeletion;
        }


        public bool Delete(AptifriedWebShoppingCartRequestDto deleteRequest) {

            bool successfulDelete = false;

            if (deleteRequest == null)
                throw new HttpException(500, "Request object missing.",
                    new ArgumentException("deleteRequest missing", "deleteRequest"));


            AptifriedWebShoppingCart cartRequested = null;
            if (deleteRequest != null && deleteRequest.Id > 0) {
                cartRequested = GetSavedShoppingCarts(deleteRequest.Id).FirstOrDefault();
            } else {
                throw new HttpException(500, "Invalid delete request Id");
            }

            if (cartRequested == null)
                throw new HttpException(500, "Cart in question does not exist.");


            foreach (var requestedLineToDelete in deleteRequest.Products) {
                var lineToRemove = AptifyApp.GetEntityObject(
                    WEB_SHOPPING_CART_DETAILS_ENTITY_NAME, 
                    requestedLineToDelete.Id);
                successfulDelete = lineToRemove.Delete();
            }

            return successfulDelete;
        }


        private IEnumerable<AptifriedWebShoppingCartDto> GetSavedCarts(int? shoppingCartId) {
            IList<AptifriedWebShoppingCartDto> savedCarts = new List<AptifriedWebShoppingCartDto>();
            if (this.User != null && this.User.Identity.IsAuthenticated) {

                IList<AptifriedWebShoppingCart> shoppingCarts = null;

                if (shoppingCartId.HasValue)
                    shoppingCarts = GetSavedShoppingCarts(shoppingCartId.Value);
                else
                    shoppingCarts = GetUncommittedUerSavedShoppingCarts();

                foreach (var cart in shoppingCarts) {

                    AptifriedWebShoppingCartDto thisCart = CreateShoppingCartDtoFromCartModel(cart);
                    savedCarts.Add(thisCart);
                }
            }
            return savedCarts;
        }

        private AptifriedWebShoppingCartDto CreateShoppingCartDtoFromCartModel(AptifriedWebShoppingCart cart) {
            AptifriedWebShoppingCartDto resultingCartDto;
            OrdersEntity orderObject;
            GetOrderEntity(cart, out resultingCartDto, out orderObject);
            
            resultingCartDto.Order = Mapper.Map(orderObject, new AptifriedOrderDto());

            return resultingCartDto;
        }

        internal void GetOrderEntity(AptifriedWebShoppingCart cart, out AptifriedWebShoppingCartDto thisCart, out OrdersEntity orderProper) {
            // move the saved shopping cart from memory into our transfer object
            thisCart = Mapper.Map(cart, new AptifriedWebShoppingCartDto());
            AptifyGenericEntityBase orderBase = AptifyApp.GetEntityObject(ORDERS_ENTITY_NAME, -1);
            orderProper = (Aptify.Applications.OrderEntry.OrdersEntity)orderBase;

            foreach (var cartLine in cart.Lines) {
                AddOrderLineSetRegistrant(ref orderProper, cartLine);
            }
        }

        private IList<AptifriedWebShoppingCart> GetUncommittedUerSavedShoppingCarts() {
            var shoppingCarts =
                session.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.OrderId is null and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedWebShoppingCart))
                    .List<AptifriedWebShoppingCart>();
            
            return shoppingCarts;
        }

        private IList<AptifriedWebShoppingCart> GetSavedShoppingCarts(int shoppingCartId) {
            var shoppingCarts =
                session.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.ID = " + shoppingCartId.ToString() +" and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedWebShoppingCart))
                    .List<AptifriedWebShoppingCart>();

            return shoppingCarts;
        }

        private IList<AptifriedWebShoppingCartDetails> GetCartDetails(int shoppingCartId) {
            var shoppingCartDetails = session.QueryOver<AptifriedWebShoppingCartDetails>()
                .Where(x => x.WebShoppingCartId == shoppingCartId)
                .List<AptifriedWebShoppingCartDetails>();

            return shoppingCartDetails;
        }

        private int GetClassIdFromProductId(int productId) {
            var classAssociatedWithProduct =
                session.QueryOver<AptifriedClass>()
                    .Where(x => x.Product.Id == productId)
                    .SingleOrDefault();

            if (classAssociatedWithProduct == null)
                throw new HttpException(500, "No product associated with class!");

            return classAssociatedWithProduct.Id;
        }

        private AptifriedWebShoppingCartDto SaveRequestedShoppingCart(AptifriedWebShoppingCartRequestDto request) {
            AptifriedWebShoppingCartDto cartDto = null;

            var aptifyShoppingCart = AptifyApp.GetEntityObject(WEB_SHOPPING_CART_ENTITY_NAME, request.Id);
            aptifyShoppingCart.SetValue("Name", request.Name);
            aptifyShoppingCart.SetValue("Description", request.Description);
            aptifyShoppingCart.SetValue("WebUserID", AptifyUser.Id);
            aptifyShoppingCart.SetValue("XmlData", "<OBJECT />");
            if (aptifyShoppingCart.Save(false)) {

                foreach (var requestedLine in request.Products) {
                    var aptifyShoppingCartDetail = AptifyApp.GetEntityObject(
                        WEB_SHOPPING_CART_DETAILS_ENTITY_NAME,
                        requestedLine.Id);

                    aptifyShoppingCartDetail.SetValue("WebShoppingCartID", aptifyShoppingCart.RecordID);
                    aptifyShoppingCartDetail.SetValue("ProductID", requestedLine.ProductId);
                    aptifyShoppingCartDetail.SetValue("RegistrantId", requestedLine.RegistrantId);

                    if (!aptifyShoppingCartDetail.Save(false))
                        throw new HttpException(500, "Could not save shopping cart line. Error: " + aptifyShoppingCartDetail.LastError);

                }
            } else {
                throw new HttpException(500, "Could not save shopping cart. Error: " + aptifyShoppingCart.LastError);
            }

            var newlyCreatedShoppingCartModel = session.QueryOver<AptifriedWebShoppingCart>()
                .Where(x => x.Id == aptifyShoppingCart.RecordID)
                .SingleOrDefault();


            return CreateShoppingCartDtoFromCartModel(newlyCreatedShoppingCartModel);
        }

        internal OrdersEntity GetOrderEntity(AptifriedWebShoppingCartRequestDto createRequest) {

            AptifyGenericEntityBase orderGe = null;
            
            long orderId = -1;

            AptifyGenericEntityBase webShoppingCart = AptifyApp.GetEntityObject(WEB_SHOPPING_CART_ENTITY_NAME, createRequest.Id);
            orderId = Convert.ToInt64(webShoppingCart.GetValue("OrderID"));

            orderGe = AptifyApp.GetEntityObject("Orders", orderId);
            var orderProper = (Aptify.Applications.OrderEntry.OrdersEntity)orderGe;
            orderProper.ShipToID = Convert.ToInt64(AptifyUser.PersonId);

            foreach (var requestedLine in createRequest.Products) {
                AddOrderLineSetRegistrant(ref orderProper, requestedLine);
            }

            return orderProper;
        }

        private void AddOrderLineSetRegistrant(
            ref OrdersEntity orderProper,
            AptifriedWebShoppingCartDetails requestedLine) {

            AptifriedWebShoppingCartProductRequestDto dtoLine = Mapper.Map(requestedLine, new AptifriedWebShoppingCartProductRequestDto());
            AddOrderLineSetRegistrant(ref orderProper, dtoLine);
        }

        private void AddOrderLineSetRegistrant(
            ref OrdersEntity orderProper, 
            AptifriedWebShoppingCartProductRequestDto requestedLine) {

            var orderLines = orderProper.AddProduct(Convert.ToInt64(requestedLine.ProductId));


            foreach (var orderLine in orderLines) {
                if (orderLine.ProductID == requestedLine.ProductId &&
                    orderLine.ExtendedOrderDetailEntity != null) {

                    // hopefully we won't have to run all of this code more than the first time
                    // we add an order line to the order. Guard all of this logic by looking at
                    // the registrant.

                    if (orderLine.ExtendedOrderDetailEntity.EntityName == "Class Registrations" &&
                        Convert.ToInt32(orderLine.ExtendedOrderDetailEntity.GetValue("StudentID")) !=
                        requestedLine.RegistrantId) {

                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("ClassID", GetClassIdFromProductId(requestedLine.ProductId));
                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("StudentID", requestedLine.RegistrantId);
                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("Status", "Registered");
                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("__ExtendedAttributeObjectData",
                           orderLine.ExtendedOrderDetailEntity.GetObjectData(false));


                    } else if (orderLine.ExtendedOrderDetailEntity.EntityName == "OrderMeetingDetail" &&
                        Convert.ToInt32(orderLine.ExtendedOrderDetailEntity.GetValue("AttendeeID")) !=
                        requestedLine.RegistrantId) {

                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("AttendeeID", requestedLine.RegistrantId);
                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("ProductID", requestedLine.ProductId);
                        orderLine.ExtendedOrderDetailEntity
                            .SetValue("RegistrationType", "Pre-Registration"); // TODO: validate that this is the correct with biz                         
                    }
                }
            }
        }
    }
}