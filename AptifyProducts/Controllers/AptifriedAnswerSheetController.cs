using AptifyWebApi.Dto;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedAnswerSheetController : AptifyEnabledApiController {

        public AptifriedAnswerSheetController(ISession session) : base(session) { }

        public IList<AptifriedAnswerSheetDto> Get() {
            //TODO: retrive
            return null;
        }

        public AptifriedAnswerSheetDto Post(AptifriedAnswerSheetDto answerSheet) {
            //TODO: save to aptify and return saved object
            return null;
        }
    }
}