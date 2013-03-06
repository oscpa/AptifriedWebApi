﻿using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers {

    [Authorize]
    public class AptifriedSavedCartController : AptifyEnabledApiController {

        public AptifriedSavedCartController(ISession session) : base(session) { }

        public IList<AptifriedSavedShoppingCartDto> Get() {
            return GetSaveShoppingCarts(null);
        }

        public IList<AptifriedSavedShoppingCartDto> Get(int shoppingCartId) {
            return GetSaveShoppingCarts(shoppingCartId);
        }

        public AptifriedShoppingCartRequestDto Put(AptifriedShoppingCartRequestDto shoppingCartToUpdate) {

            if (shoppingCartToUpdate == null) 
                throw new HttpException(500, 
                    "Cart to Update must not be empty.", new ArgumentException("shoppingCartToUpdate"));

            // TODO: Refactor the other shopping cart controller to dry the code that parses products and creates xml
            if (shoppingCartToUpdate.Products != null)
                throw new HttpException("Don't be tryin to update an order's products in this endpoint... until we refactor" +
                    " and dry out the methods that turn order lines into xml.");

            var shoppingCartGe = AptifyApp.GetEntityObject("Web Shopping Carts", shoppingCartToUpdate.Id);

            if (shoppingCartToUpdate.Id >= 0 && shoppingCartGe.RecordID != shoppingCartToUpdate.Id) {
                throw new HttpException(500, "Could not find shopping cart id:" + shoppingCartToUpdate.Id.ToString());
            }

            // TODO: look at moving "Set/add Values" calls into automapper.
            shoppingCartGe.SetAddValue("Name", shoppingCartToUpdate.Name);
            shoppingCartGe.SetAddValue("WebShoppingCartTypeID", shoppingCartToUpdate.Type.Id);
            shoppingCartGe.SetAddValue("Description", shoppingCartToUpdate.Type.Id);
            // TODO: try to figure out how best to implement sharing shopping carts
            // shoppingCartGe.SetAddValue("WebUserID", ???); 

            string errorMesssage = string.Empty;
            if (!shoppingCartGe.Save(ref errorMesssage)) {
                throw new HttpException(500, "Aptify Error saving cart. Error is: " + errorMesssage + 
                Environment.NewLine + "Last GE Error: " + shoppingCartGe.LastError);
            }

            return shoppingCartToUpdate;

        }


        private IList<AptifriedSavedShoppingCartDto> GetSaveShoppingCarts(int? shoppingCartId) {
            var resultingShoppingCarts = new List<AptifriedSavedShoppingCartDto>();

            if (shoppingCartId.HasValue) {
                var shoppingCarts =
                    session.CreateSQLQuery(
                        "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                        " where carts.ID = " + shoppingCartId.ToString() + " and users.UserID = '" + User.Identity.Name + "'")
                        .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                        .List<AptifriedSavedShoppingCart>();
                resultingShoppingCarts = Mapper.Map(shoppingCarts, new List<AptifriedSavedShoppingCartDto>());
            } else {
                var shoppingCarts =
                   session.CreateSQLQuery(
                       "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                       " where users.UserID = '" + User.Identity.Name + "'")
                       .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                       .List<AptifriedSavedShoppingCart>();

                resultingShoppingCarts = Mapper.Map(shoppingCarts, new List<AptifriedSavedShoppingCartDto>());
            }
            return resultingShoppingCarts;
        }
    }

}