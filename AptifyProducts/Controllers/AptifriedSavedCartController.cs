using AptifyWebApi.Dto;
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

        public IList<AptifriedSavedShoppingCartDto> Get(int? shoppingCartId) {
            return GetSaveShoppingCarts(shoppingCartId);
        }



        public AptifriedSavedShoppingCartDto Put(AptifriedSavedShoppingCartDto shoppingCart) {

            return null;

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