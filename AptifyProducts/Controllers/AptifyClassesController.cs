#region using

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Repository;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifyClassesController : Controller
    {
        private readonly IAptifriedClassRepository aptifyRepository;
        private ISession session;

        public AptifyClassesController(ISession session)
        {
            this.session = session;
            aptifyRepository = new HibernatedAptifriedClassRepository(session);
        }


        // GET: /AptifyClass/
        public ActionResult Index()
        {
            //var hibernatedCol = aptifyRepository.GetAll();
            IList<AptifriedClassDto> classDto = new List<AptifriedClassDto>();
            //classDto = Mapper.Map(hibernatedCol, new List<AptifriedClassDto>());

            ViewBag.RouteUri = "/api/aptifriedclasses";


            return View(classDto.AsEnumerable());
        }

        public ActionResult GetSession(int classId)
        {
            var decendants = aptifyRepository.GetDecendants(classId);
            IList<AptifriedClassDto> classesDto = new List<AptifriedClassDto>();
            classesDto = Mapper.Map(decendants, new List<AptifriedClassDto>());

            ViewBag.RouteUri = "/api/aptifriedclasses/getsessions";

            return View("SessionList", classesDto);
        }
    }
}