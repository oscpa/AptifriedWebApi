using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Repository;
using AptifyWebApi.Attributes;
using NHibernate;
using System.Web.Mvc;
using System.Web.Http;


namespace AptifyWebApi.Controllers {

    [System.Web.Http.Authorize]
    public class AptifriedCartController : ApiController {

        private ISession _sesssion;
        private IAptifriedClassRepository _repo;

        public AptifriedCartController(ISession session) {
            _sesssion = session;
            _repo = new HibernatedAptifriedClassRepository(session);
        }

        
        public IQueryable<AptifriedProductDto> Get() {
            return null;
        }


        public void Put(IList<AptifriedProductDto> product) {
            
        }
    }
}