using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using AptifyWebApi.Repository;
using AptifyWebApi.Attributes;
using NHibernate;


namespace AptifyWebApi.Controllers {
    public class AptifriedCartController : ApiController {

        private ISession _sesssion;
        private IAptifriedClassRepository _repo;

        public AptifriedCartController(ISession session) {
            _sesssion = session;
            _repo = new HibernatedAptifriedClassRepository(session);
        }

        public IQueryable<AptifriedProductDto> Get() {
            var userName = HttpContext.Current.User.Identity.Name;
            return null;
        }

        public void Put(AptifriedProductDto product) {
            
            
        }
    }
}