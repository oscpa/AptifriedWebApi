#region using

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedWebCartController : AptifyEnabledApiController
    {
        public AptifriedWebCartController(ISession session) : base(session)
        {
        }

        public IList<AptifriedWebShoppingCartDto> Get()
        {
            return GetSaveShoppingCarts(null);
        }

        public IList<AptifriedWebShoppingCartDto> Get(int shoppingCartId)
        {
            return GetSaveShoppingCarts(shoppingCartId);
        }

        public AptifriedWebShoppingCartRequestDto Put(AptifriedWebShoppingCartRequestDto shoppingCartToUpdate)
        {
            if (shoppingCartToUpdate == null)
                throw new HttpException(500,
                                        "Cart to Update must not be empty.",
                                        new ArgumentException("shoppingCartToUpdate"));

            // TODO: Refactor the other shopping cart controller to dry the code that parses products and creates xml
            if (shoppingCartToUpdate.Products != null)
                throw new HttpException(
                    "Don't be tryin to update an order's products in this endpoint... until we refactor" +
                    " and dry out the methods that turn order lines into xml.");

            var shoppingCartGe = AptifyApp.GetEntityObject("Web Shopping Carts", shoppingCartToUpdate.Id);

            if (shoppingCartToUpdate.Id >= 0 && shoppingCartGe.RecordID != shoppingCartToUpdate.Id)
            {
                throw new HttpException(500, "Could not find shopping cart id:" + shoppingCartToUpdate.Id.ToString());
            }

            // TODO: look at moving "Set/add Values" calls into automapper.
            if (shoppingCartToUpdate.Name != null)
                shoppingCartGe.SetAddValue("Name", shoppingCartToUpdate.Name);

            if (shoppingCartToUpdate.ShoppingCartType != null && shoppingCartToUpdate.ShoppingCartType.Id != null)
                shoppingCartGe.SetAddValue("WebShoppingCartTypeID", shoppingCartToUpdate.ShoppingCartType.Id);

            if (shoppingCartToUpdate.Description != null)
                shoppingCartGe.SetAddValue("Description", shoppingCartToUpdate.Description);
            // TODO: try to figure out how best to implement sharing shopping carts
            // shoppingCartGe.SetAddValue("WebUserID", ???); 

            string errorMesssage = string.Empty;
            if (!shoppingCartGe.Save(ref errorMesssage))
            {
                throw new HttpException(500, "Aptify Error saving cart. Error is: " + errorMesssage +
                                             Environment.NewLine + "Last GE Error: " + shoppingCartGe.LastError);
            }

            return shoppingCartToUpdate;
        }


        private IList<AptifriedWebShoppingCartDto> GetSaveShoppingCarts(int? shoppingCartId)
        {
            var resultingShoppingCarts = new List<AptifriedWebShoppingCartDto>();

            if (shoppingCartId.HasValue)
            {
                var shoppingCarts =
                    session.CreateSQLQuery(
                        "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                        " where carts.ID = " + shoppingCartId.ToString() + " and users.UserID = '" + User.Identity.Name +
                        "'")
                           .AddEntity("carts", typeof (AptifriedWebShoppingCart))
                           .List<AptifriedWebShoppingCart>();
                resultingShoppingCarts = Mapper.Map(shoppingCarts, new List<AptifriedWebShoppingCartDto>());
            }
            else
            {
                var shoppingCarts =
                    session.CreateSQLQuery(
                        "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                        " where users.UserID = '" + User.Identity.Name + "'")
                           .AddEntity("carts", typeof (AptifriedWebShoppingCart))
                           .List<AptifriedWebShoppingCart>();

                resultingShoppingCarts = Mapper.Map(shoppingCarts, new List<AptifriedWebShoppingCartDto>());
            }
            return resultingShoppingCarts;
        }
    }
}