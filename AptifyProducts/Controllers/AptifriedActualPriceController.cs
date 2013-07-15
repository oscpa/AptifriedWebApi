#region using

using System;
using System.Web;
using Aptify.Applications.OrderEntry;
using AptifyWebApi.Models.Dto;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedActualPriceController : AptifyEnabledApiController
    {
        public AptifriedActualPriceController(ISession session) : base(session)
        {
        }

        public AptifriedOrderDto Post(AptifriedWebShoppingCartRequestDto addRequest)
        {
            // Is this necessary?
            if (addRequest == null)
                throw new HttpException(500, "Add Request not present.", new ArgumentException("addRequest"));

            var orderObj = (OrdersEntity) AptifyApp.GetEntityObject("Orders", -1);

            orderObj.EmployeeID = 1; // Aptify_Ebiz

            if (AptifyUser != null)
            {
                orderObj.ShipToID = AptifyUser.PersonId;
            }
            if (addRequest.Products != null && addRequest.Products.Count > 0)
            {
                orderObj.ShipToID = addRequest.Products[0].RegistrantId;
            }

            foreach (var addProd in addRequest.Products)
            {
                orderObj.AddProduct(addProd.ProductId);
            }

            return Mapper.Map(orderObj, new AptifriedOrderDto());
        }
    }
}