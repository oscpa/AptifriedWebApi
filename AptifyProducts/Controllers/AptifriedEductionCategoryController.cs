#region using

using System.Collections.Generic;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedEductionCategoryController : AptifyEnabledApiController
    {
        public AptifriedEductionCategoryController(ISession session) : base(session)
        {
        }

        public IList<AptifriedEducationCategoryDto> Get()
        {
            var educationUnits = session.QueryOver<AptifriedEducationCategory>()
                                        .List();

            return Mapper.Map(educationUnits, new List<AptifriedEducationCategoryDto>());
        }
    }
}