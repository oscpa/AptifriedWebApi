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


namespace AptifyWebApi.Controllers {

    [System.Web.Http.Authorize]
    public class AptifriedCartController : AptifyEnabledApiController {

        public AptifriedCartController(ISession session) : base(session) { }

        public IEnumerable<AptifriedSavedShoppingCartDto> Get(int cartId) {
            return GetSavedCarts(cartId);
        }
        
        public IEnumerable<AptifriedSavedShoppingCartDto> Get() {
            return GetSavedCarts(null);
        }

        public AptifriedSavedShoppingCartDto Post(AptifriedShoppingCartRequestDto addRequest) {
            
            // TODO: refactor all this.

            if (addRequest == null)
                throw new HttpException(500, "Add Request not present.", new ArgumentException("addRequest"));

            AptifyGenericEntityBase orderGe = null;
            AptifyGenericEntityBase aptifyShoppingCartGe = null;
            var aptifyXmlParser = new Aptify.Framework.BusinessLogic.GenericEntity.XMLParser();
            aptifyXmlParser.UserCredential = this.AptifyApp.UserCredentials;

            if (addRequest.Id >= 0) {

                var userCartCheck = GetCarts(addRequest.Id);

                if (userCartCheck.Count == 0)
                    throw new HttpException(500, "User does not have access to this cart.");

                aptifyShoppingCartGe = AptifyApp.GetEntityObject("Web Shopping Carts", addRequest.Id);
            } else {
                aptifyShoppingCartGe = AptifyApp.GetEntityObject("Web Shopping Carts", -1);
                aptifyShoppingCartGe.SetAddValue("Name", AptifyUser.UserName + ' ' + DateTime.Now.ToLongDateString());
                aptifyShoppingCartGe.SetAddValue("WebUserID", Convert.ToInt64(AptifyUser.Id));
            }

            // TODO: need to refactor this so we don't have duplicate parsing code.
            string xmlData = Convert.ToString(aptifyShoppingCartGe.GetValue("XMLData"));
            if (!string.IsNullOrEmpty(xmlData)) {
                orderGe = AptifyApp.GetEntityObject("Orders", -1);
                aptifyXmlParser.LoadGEFromXMLString(xmlData, ref orderGe); 
            }
            if (orderGe == null) {
                try {
                    orderGe = AptifyApp.GetEntityObject("Orders", -1L);
                } catch (Exception ex) {
                    throw new HttpException(500, "error generating order", ex);
                }
                
            }
            var orderProper = (Aptify.Applications.OrderEntry.OrdersEntity)orderGe;

            if (orderProper.EmployeeID <= 0)
                orderProper.EmployeeID = 1; // should be aptify ebiz

            orderProper.ShipToID = Convert.ToInt64(AptifyUser.PersonId);
            foreach (var requestedProductId in addRequest.Products) {
                var orderLines = orderProper.AddProduct(Convert.ToInt64(requestedProductId.ProductId));

                foreach (var orderLine in orderLines) {
                    if (orderLine.ProductID == requestedProductId.ProductId && 
                        orderLine.ExtendedOrderDetailEntity != null) {
                            
                        // hopefully we won't have to run all of this code more than the first time
                        // we add an order line to the order. Guard all of this logic by looking at
                        // the registrant.
    
                        if (orderLine.ExtendedOrderDetailEntity.EntityName == "Class Registrations" &&
                            Convert.ToInt32(orderLine.ExtendedOrderDetailEntity.GetValue("StudentID")) != 
                            requestedProductId.RegistrantId) {

                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("ClassID", GetClassIdFromProductId(requestedProductId.ProductId));
                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("StudentID", requestedProductId.RegistrantId);
                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("Status", "Registered");
                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("__ExtendedAttributeObjectData",
                                   orderLine.ExtendedOrderDetailEntity.GetObjectData(false));


                        } else if (orderLine.ExtendedOrderDetailEntity.EntityName == "OrderMeetingDetail" &&
                            Convert.ToInt32(orderLine.ExtendedOrderDetailEntity.GetValue("AttendeeID")) != 
                            requestedProductId.RegistrantId) {

                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("AttendeeID", requestedProductId.RegistrantId);
                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("ProductID", requestedProductId.ProductId);
                                orderLine.ExtendedOrderDetailEntity
                                    .SetValue("RegistrationType", "Pre-Registration"); // TODO: validate that this is the correct with biz                         
                        }
                    }
                }

            }

            string convertedXmlData = string.Empty;
            if (aptifyXmlParser.CreateXMLStream(ref convertedXmlData, orderProper, true, false)) {
                aptifyShoppingCartGe.SetAddValue("XmlData", convertedXmlData);
            } else {
                throw new HttpException(500, "Could not convert order to xml to save cart.");
            }

            string possibleError = string.Empty;
            if (!aptifyShoppingCartGe.Save(ref possibleError)) {
                throw new HttpException(500, "Error saving shopping cart. Error was: " + possibleError);
            }

            AptifriedSavedShoppingCartDto savedCart = new AptifriedSavedShoppingCartDto() {
                Id = Convert.ToInt32(aptifyShoppingCartGe.RecordID),
                Name = Convert.ToString(aptifyShoppingCartGe.GetValue("Name")),
                Description = Convert.ToString(aptifyShoppingCartGe.GetValue("Description")),
                DateCreated = Convert.ToDateTime(aptifyShoppingCartGe.GetValue("DateCreated")),
                DateUpdated = Convert.ToDateTime(aptifyShoppingCartGe.GetValue("DateUpdated")),
                OrderId = Convert.ToInt32(aptifyShoppingCartGe.GetValue("OrderID")),
                Order = Mapper.Map(orderProper, new AptifriedOrderDto())
            };

            return savedCart;
        }

        public bool Delete(int cartId) {
            var myCarts = GetCarts(cartId);
            bool successInDeletion = false;

            if (myCarts != null && myCarts.Count == 1) {
                var cartToDelete = AptifyApp.GetEntityObject("Web Shopping Carts", Convert.ToInt64(cartId));    
                successInDeletion = cartToDelete.Delete();
            }

            return successInDeletion;
        }


        public bool Delete(AptifriedShoppingCartRequestDto deleteRequest) {

            bool successfulDelete = false;

            if (deleteRequest == null)
                throw new HttpException(500, "Request object missing.",
                    new ArgumentException("deleteRequest missing", "deleteRequest"));


            AptifriedSavedShoppingCart cartRequested = null;
            if (deleteRequest != null && deleteRequest.Id > 0) {
                cartRequested = GetCarts(deleteRequest.Id).FirstOrDefault();
            } else {
                throw new HttpException(500, "Invalid delete request Id");
            }

            if (cartRequested == null)
                throw new HttpException(500, "Cart in question does not exist.");

            var aptifyXmlparser = new Aptify.Framework.BusinessLogic.GenericEntity.XMLParser();
            aptifyXmlparser.UserCredential = AptifyApp.UserCredentials;
            AptifyGenericEntityBase orderBase = AptifyApp.GetEntityObject("Orders", -1);

            if (aptifyXmlparser.LoadGEFromXMLString(cartRequested.XmlData, ref orderBase)) {

                foreach (AptifyGenericEntityBase orderLineBase in orderBase.SubTypes["OrderLines"]) {
                    foreach (var orderLineToDelete in deleteRequest.Products) {

                        var orderLineProper = (Aptify.Applications.OrderEntry.OrderLinesEntity)orderLineBase;

                        if (orderLineProper.ExtendedOrderDetailEntity.EntityName == "Class Registrations" &&
                            Convert.ToInt32(orderLineProper.ExtendedOrderDetailEntity.GetValue("StudentID")) !=
                            orderLineToDelete.RegistrantId) {

                            successfulDelete = orderLineProper.Delete();
                            break;

                        } else if (orderLineProper.ExtendedOrderDetailEntity.EntityName == "OrderMeetingDetail" &&
                            Convert.ToInt32(orderLineProper.ExtendedOrderDetailEntity.GetValue("AttendeeID")) !=
                            orderLineToDelete.RegistrantId) {

                            successfulDelete = orderLineProper.Delete();
                            break;
                        }
                    }
                }

                // now save the order back into the saved shopping carts
                AptifyGenericEntityBase savedShoppingCart = AptifyApp.GetEntityObject("Web Saved Shopping Carts", deleteRequest.Id);
                string convertedXmlData = string.Empty;
                if (aptifyXmlparser.CreateXMLStream(ref convertedXmlData, orderBase, true, false)) {
                    savedShoppingCart.SetAddValue("XmlData", convertedXmlData);
                } else {
                    throw new HttpException(500, "Could not convert order to xml to save cart.");
                }

                successfulDelete = savedShoppingCart.Save();

            } else {
                throw new HttpException(500, "Could not load saved cart.");
            }

            return successfulDelete;
        }


        private IEnumerable<AptifriedSavedShoppingCartDto> GetSavedCarts(int? shoppingCartId) {
            IList<AptifriedSavedShoppingCartDto> savedCarts = new List<AptifriedSavedShoppingCartDto>();

            if (this.User != null && this.User.Identity.IsAuthenticated) {

                IList<AptifriedSavedShoppingCart> shoppingCarts = null;

                if (shoppingCartId.HasValue)
                    shoppingCarts = GetCarts(shoppingCartId.Value);
                else
                    shoppingCarts = GetUncommittedUerSavedShoppingCarts();

                foreach (var cart in shoppingCarts) {

                    AptifriedSavedShoppingCartDto thisCart = CreateShoppingCartDtoFromCartModel(cart);

                    savedCarts.Add(thisCart);
                }
            }
            return savedCarts;
        }

        private AptifriedSavedShoppingCartDto CreateShoppingCartDtoFromCartModel(AptifriedSavedShoppingCart cart) {
            // move the saved shopping cart from memory into our transfer object
            AptifriedSavedShoppingCartDto thisCart = Mapper.Map(cart, new AptifriedSavedShoppingCartDto());

            // TOOD: Try to move this XML Parsing code over to the automapper config to generate an order 
            // parse the order within the xml over to an order object
            var aptifyXmlParser = new Aptify.Framework.BusinessLogic.GenericEntity.XMLParser();
            aptifyXmlParser.UserCredential = this.AptifyApp.UserCredentials;
            AptifyGenericEntityBase orderBase = AptifyApp.GetEntityObject("Orders", -1);

            // if the xml makes for a valid aptify ge order, then add it to the resulting cart
            if (aptifyXmlParser.LoadGEFromXMLString(cart.XmlData, ref orderBase)) {
                var orderObject = (Aptify.Applications.OrderEntry.OrdersEntity)orderBase;
                thisCart.Order = Mapper.Map(orderObject, new AptifriedOrderDto());
            }
            return thisCart;
        }

        /// <summary>
        /// Retrieves shopping carts that are not committed as orders
        /// </summary>
        /// <returns></returns>
        private IList<AptifriedSavedShoppingCart> GetUncommittedUerSavedShoppingCarts() {
            var shoppingCarts =
                session.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.OrderId is null and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                    .List<AptifriedSavedShoppingCart>();
            
            return shoppingCarts;
        }

        private IList<AptifriedSavedShoppingCart> GetCarts(int shoppingCartId) {
            var shoppingCarts =
                session.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.ID = " + shoppingCartId.ToString() +" and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                    .List<AptifriedSavedShoppingCart>();

            return shoppingCarts;
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
    }
}