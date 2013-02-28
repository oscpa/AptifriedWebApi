using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AptifyWebApi.Dto;
using NHibernate;
using AptifyWebApi.Models;
using System.Web;
using AutoMapper;

namespace AptifyWebApi.Controllers
{
    public class AptifriedPaymentTypeController : ApiController
    {
        private ISession _session;

        public AptifriedPaymentTypeController(ISession session) {
            _session = session;
        }

        public IEnumerable<AptifriedPaymentTypeDto> Get() {
            return GetPaymentTypes(null);
        }

        public IEnumerable<AptifriedPaymentTypeDto> Get(int paymentTypeId) {
            return GetPaymentTypes(paymentTypeId);
       }

        private IList<AptifriedPaymentTypeDto> GetPaymentTypes(int? paymentTypeId) {
            IList<AptifriedPaymentTypeDto> paymentTypesToReturn = new List<AptifriedPaymentTypeDto>();

            if (paymentTypeId.HasValue) {

                var singlePaymentType = _session.QueryOver<AptifriedPaymentType>()
                    .Where(x => x.Id == paymentTypeId.Value)
                    .SingleOrDefault();

                if (singlePaymentType == null)
                    throw new HttpException(500, "Could not find payment type: " + paymentTypeId.Value);

                paymentTypesToReturn.Add(Mapper.Map(singlePaymentType, new AptifriedPaymentTypeDto()));
            } else {
                var allPaymentTypes = _session.QueryOver<AptifriedPaymentType>()
                    .Where(x => x.Type == "Credit Card")
                    .List<AptifriedPaymentType>();

                Mapper.Map(allPaymentTypes, paymentTypesToReturn);
            }
            return paymentTypesToReturn;
        }
    }
}
