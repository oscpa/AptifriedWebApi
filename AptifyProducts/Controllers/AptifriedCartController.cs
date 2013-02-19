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
    public class AptifriedCartController : ApiController {

        private ISession _sesssion;
        private Aptify.Framework.Application.AptifyApplication _app;
        private AptifriedAuthroizedUserDto _aptifyUser;

        protected AptifriedAuthroizedUserDto AptifyUser {
            get {
                if (_aptifyUser == null && this.User.Identity.IsAuthenticated) {
                    var thisUser = _sesssion.QueryOver<AptifriedWebUser>()
                        .Where(u => u.UserName == User.Identity.Name)
                        .SingleOrDefault();
                    _aptifyUser = Mapper.Map(thisUser, new AptifriedAuthroizedUserDto());
                }
                return _aptifyUser;
            }
        }

        protected Aptify.Framework.Application.AptifyApplication AptifyApp {
            get {
                if (_app == null) {
                    
                    _app = new Aptify.Framework.Application.AptifyApplication(
                        AptifriedAuthorizationFactory.GetUserCredientails());
                }
                return _app;
            }

        }

        public AptifriedCartController(ISession session) {
            _sesssion = session;
        }

        public IEnumerable<AptifriedSavedShoppingCartDto> Get(int cartId) {
            return GetSavedCarts(cartId);
        }
        
        public IEnumerable<AptifriedSavedShoppingCartDto> Get() {
            return GetSavedCarts(null);
        }

        [System.Web.Http.HttpPost]
        public AptifriedSavedShoppingCartDto Post(AptifriedShoppingCartAddRequestDto addRequest) {
            
            // TODO: refactor all this.

            if (addRequest == null)
                throw new HttpException(500, "Add Request not present.", new ArgumentException("addRequest"));

            AptifyGenericEntityBase orderGe = null;
            AptifyGenericEntityBase aptifyShoppingCartGe = null;
            var aptifyXmlParser = new Aptify.Framework.BusinessLogic.GenericEntity.XMLParser();


            if (addRequest.Id >= 0) {

                var userCartCheck = _sesssion.QueryOver<AptifriedSavedShoppingCart>()
                    .Where(x => x.Id == addRequest.Id)
                    .JoinQueryOver(x => x.WebUser)
                    .Where(wu => wu.UserName == this.User.Identity.Name)
                    .RowCount();

                if (userCartCheck == 0)
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

            orderProper.ShipToID = Convert.ToInt64(AptifyUser.PersonId);
            foreach (var requestedProductId in addRequest.Products) {
                orderProper.AddProduct(Convert.ToInt64(requestedProductId.ProductId));
            }

            aptifyShoppingCartGe.SetAddValue("XmlData",
                ((Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntity)orderProper).GetDataString());

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



        private IEnumerable<AptifriedSavedShoppingCartDto> GetSavedCarts(int? shoppingCartId) {
            IList<AptifriedSavedShoppingCartDto> savedCarts = new List<AptifriedSavedShoppingCartDto>();

            if (this.User != null && this.User.Identity.IsAuthenticated) {

                IList<AptifriedSavedShoppingCart> shoppingCarts = null;

                if (shoppingCartId.HasValue)
                    shoppingCarts = GetCarts(shoppingCartId.Value);
                else
                    shoppingCarts = GetUncommittedUerSavedShoppingCarts();

                foreach (var cart in shoppingCarts) {

                    // move the saved shopping cart from memory into our transfer object
                    AptifriedSavedShoppingCartDto thisCart = Mapper.Map(cart, new AptifriedSavedShoppingCartDto());
                    
                    // TOOD: Try to move this XML Parsing code over to the automapper config to generate an order 
                    // parse the order within the xml over to an order object
                    var aptifyXmlParser = new Aptify.Framework.BusinessLogic.GenericEntity.XMLParser();
                    AptifyGenericEntityBase orderBase = null;
                   
                    // if the xml makes for a valid aptify ge order, then add it to the resulting cart
                    if (aptifyXmlParser.LoadGEFromXMLString(cart.XmlData, ref orderBase)) {
                        var orderObject = (Aptify.Applications.OrderEntry.OrdersEntity)orderBase;
                        thisCart.Order = Mapper.Map(orderObject, new AptifriedOrderDto());
                    }

                    savedCarts.Add(thisCart);
                }
            }
            return savedCarts;
        }

        /// <summary>
        /// Retrieves shopping carts that are not committed as orders
        /// </summary>
        /// <returns></returns>
        private IList<AptifriedSavedShoppingCart> GetUncommittedUerSavedShoppingCarts() {
            var shoppingCarts =
                _sesssion.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.OrderId is null and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                    .List<AptifriedSavedShoppingCart>();
            
            return shoppingCarts;
        }

        private IList<AptifriedSavedShoppingCart> GetCarts(int shoppingCartId) {
            var shoppingCarts =
                _sesssion.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.ID = " + shoppingCartId.ToString() +" and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                    .List<AptifriedSavedShoppingCart>();

            return shoppingCarts;
        }
    }
}