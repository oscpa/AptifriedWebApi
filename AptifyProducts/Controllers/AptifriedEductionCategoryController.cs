using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Aptifried;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    public class AptifriedEductionCategoryController : AptifyEnabledApiController {
        public AptifriedEductionCategoryController(ISession session) : base(session) { }

        public IList<AptifriedEducationCategoryDto> Get() {
            var educationUnits = session.QueryOver<AptifriedEducationCategory>()
                .List();

            return Mapper.Map(educationUnits, new List<AptifriedEducationCategoryDto>());
        }
    }
}