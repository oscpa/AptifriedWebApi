using AptifyWebApi.Models;
using AptifyWebApi.Repository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using AptifyWebApi.Dto;

namespace AptifyWebApi.Controllers {
    public class AptifriedClassesController : ApiController {

        private IAptifriedClassRepository _repo;
        private ISession session;
        public AptifriedClassesController(ISession session) {
            this.session = session;
            _repo = new HibernatedAptifriedClassRepository(this.session);
        }

        public IQueryable<AptifriedClassDto> Get() {
            var hibernatedCol = _repo.GetAll();
            IList<AptifriedClassDto> classDto = new List<AptifriedClassDto>();
            classDto = Mapper.Map(hibernatedCol, new List<AptifriedClassDto>());
            return classDto.AsQueryable<AptifriedClassDto>();
        }

        public AptifriedClassDto Put(AptifriedClassDto classDto) {
            if (classDto != null) {
                classDto.Name += "With juice!";
            }
            return classDto;
        }

        public IQueryable<AptifriedClassDto> GetSessions(int classId) {
            var decendants = _repo.GetDecendants(classId);

            IList<AptifriedClassDto> classDto = new List<AptifriedClassDto>();
            classDto = Mapper.Map(decendants, new List<AptifriedClassDto>());

            return classDto.AsQueryable();
        }


    }
}
