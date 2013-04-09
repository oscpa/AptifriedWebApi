using AptifyWebApi.Dto;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedCompletedOrderController : AptifyEnabledApiController {
        public AptifriedCompletedOrderController(ISession session) : base(session) { }
        
        public IEnumerable<AptifriedCompletedOrderDto> Get() {
            return null;
        }
    }
}