#region using

using System.Collections.Generic;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Aptifried;
<<<<<<< HEAD
=======
using AptifyWebApi.Models.Dto;
>>>>>>> origin/ac-init
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