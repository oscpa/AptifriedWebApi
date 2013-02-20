﻿using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers
{
    public class AptifriedCartSubmitController : ApiController
    {
        // TODO: extract GetCarts/etc into class and make DRY

        private ISession _session;
        public AptifriedCartSubmitController(ISession session) {
            this._session = session;
        }

        public AptifriedOrderDto Post(AptifriedShoppingCartSubmitRequestDto submitRequest) {
            var existingCarts = GetCarts(submitRequest.SavedShoppingCartId);

            if (existingCarts.Count > 1) 
                throw new HttpException(500, "More than one cart returned from the id... what?! How?!");

            if (existingCarts.Count == 0)
                throw new HttpException(500, "No carts returned");

            if (existingCarts[0].OrderId > 0)
                throw new HttpException(500, "This cart has already been submitted");

            if (string.IsNullOrEmpty(existingCarts[0].XmlData))
                throw new HttpException(500, "This cart doesn't seem to have an order associated with it.");

            var aptifyXmlParser = new Aptify.Framework.BusinessLogic.GenericEntity.XMLParser();
            Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase orderGe = null;

            if (aptifyXmlParser.LoadGEFromXMLString(existingCarts[0].XmlData, ref orderGe)) {
                
                // TODO : add payment info and ship

            } else {
                throw new HttpException(500, "Could not parse order associated with cart!");
            }

            return null;
            
        }

        private IList<AptifriedSavedShoppingCart> GetCarts(int shoppingCartId) {
            var shoppingCarts =
                _session.CreateSQLQuery(
                    "select carts.* from vwWebShoppingCarts carts join vwWebUsers users on carts.WebUserID = users.ID " +
                    " where carts.ID = " + shoppingCartId.ToString() + " and users.UserID = '" + User.Identity.Name + "'")
                    .AddEntity("carts", typeof(AptifriedSavedShoppingCart))
                    .List<AptifriedSavedShoppingCart>();

            return shoppingCarts;
        }
    }
}
