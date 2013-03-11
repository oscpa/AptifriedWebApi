using AptifyWebApi.Dto;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedExamController : AptifyEnabledApiController {

        public AptifriedExamController(ISession session) : base(session) { }

        public IList<AptifriedExamDto> Get() {
            //TODO: Retrieve exams from models
            return null;
        }

    }
}