using AptifyWebApi.Dto;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedConferenceMeetingOrderSesSelController : AptifyEnabledApiController {
        public AptifriedConferenceMeetingOrderSesSelController(ISession session) : base(session) { }

        public IList<AptifriedOrderDto> Get() {

        }
    }
}