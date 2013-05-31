using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {

    [System.Web.Http.Authorize]
    public class AptifriedCartDetailController : AptifyEnabledApiController {

        public AptifriedCartDetailController(ISession session) : base(session) { }

        private const string SHOPPING_CART_DETAIL_ENTITYNAME = "Web Shopping Cart Details";

        public AptifriedWebShoppingCartDto Post(AptifriedWebShoppingCartProductRequestDto cartLine) {

            if (cartLine == null)
                throw new HttpException(500, "Missing cart Line");

            var shoppingCartLine = AptifyApp.GetEntityObject(SHOPPING_CART_DETAIL_ENTITYNAME, cartLine.Id);

            if (shoppingCartLine.RecordID != cartLine.Id)
                throw new HttpException(500, "Could not find cart line.");

            int cartId = Convert.ToInt32(shoppingCartLine.GetValue("WebShoppingCartID"));

            shoppingCartLine.SetValue("ProductID", cartLine.ProductId);
            shoppingCartLine.SetValue("RegistrantID", cartLine.RegistrantId);
			if (cartLine.Campaign != null && cartLine.Campaign.Id > 0)
				shoppingCartLine.SetValue("CampaignID", cartLine.Campaign.Id);

            if (!shoppingCartLine.Save(false)) {
                throw new HttpException(500, "Error saving cart line update: " + shoppingCartLine.LastError);
            }

            return GetCart(cartId);

        }

        public AptifriedWebShoppingCartDto Delete(AptifriedWebShoppingCartProductRequestDto cartLine) {
            if (cartLine == null)
                throw new HttpException(500, "Cart Line object missing.");

            return Delete(cartLine.Id);
        }

        public AptifriedWebShoppingCartDto Delete(int webShoppingCartDetailId) {
            if (webShoppingCartDetailId < 0)
                throw new HttpException(500, "Invalid webShoppingCartDetailId: " + webShoppingCartDetailId.ToString());

            var shoppingCartLine = AptifyApp.GetEntityObject(
                SHOPPING_CART_DETAIL_ENTITYNAME, 
                Convert.ToInt64(webShoppingCartDetailId));

            if (shoppingCartLine.RecordID != webShoppingCartDetailId)
                throw new HttpException(500, "Shopping cart line does not exist.");

            int cartId = Convert.ToInt32(shoppingCartLine.GetValue("WebShoppingCartID"));

            if (!shoppingCartLine.Delete())
                throw new HttpException(500, "Could not delete cart item, error: " + shoppingCartLine.LastError);

            return GetCart(cartId);
        }



        private AptifriedWebShoppingCartDto GetCart(int cartId) {
            var cartReturned = session.QueryOver<AptifriedWebShoppingCart>()
                .Where(x => x.Id == cartId)
                .SingleOrDefault();

            return AutoMapper.Mapper.Map(cartReturned, new AptifriedWebShoppingCartDto());
        }
    }
}