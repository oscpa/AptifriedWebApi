#region using

using System.Collections.Generic;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    /// <summary>
    /// Used to return carts without order objects. The process of generating the order lines
    /// is extremely resource intensive and for instances like finding the number of total items
    /// that a user has added to their cart, we can be a little more relaxed about what we return.
    /// </summary>
    [Authorize]
    public class AptifriedCartLiteController : AptifyEnabledApiController
    {
        public AptifriedCartLiteController(ISession session) : base(session)
        {
        }

        public IList<AptifriedWebShoppingCartDto> Get()
        {
            var shoppingCarts =
                session.CreateSQLQuery("select carts.* from vwWebShoppingCarts carts " +
                                       " where carts.WebUserID = :userId and carts.OrderId is null ")
                       .AddEntity("carts", typeof (AptifriedWebShoppingCart))
                       .SetInt32("userId", AptifyUser.Id)
                       .List<AptifriedWebShoppingCart>();

            return Mapper.Map(shoppingCarts, new List<AptifriedWebShoppingCartDto>());
        }

        public IList<AptifriedWebShoppingCartDto> Get(int cartId)
        {
            var shoppingCarts =
                session.CreateSQLQuery("select carts.* from vwWebShoppingCarts carts " +
                                       " where carts.WebUserID = :userId and carts.Id = :cartId ")
                       .AddEntity("carts", typeof (AptifriedWebShoppingCart))
                       .SetInt32("userId", AptifyUser.Id)
                       .SetInt32("cartId", cartId)
                       .List<AptifriedWebShoppingCart>();

            return Mapper.Map(shoppingCarts, new List<AptifriedWebShoppingCartDto>());
        }
    }
}