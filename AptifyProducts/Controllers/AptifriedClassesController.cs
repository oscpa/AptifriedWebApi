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
using NHibernate.OData;

namespace AptifyWebApi.Controllers {
    public class AptifriedClassesController : ApiController {

        private IAptifriedClassRepository _repo;
        private ISession session;
        public AptifriedClassesController(ISession session) {
            this.session = session;
            _repo = new HibernatedAptifriedClassRepository(this.session);
        }

        public IEnumerable<AptifriedClassDto> Get() {

            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria;
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedClass>
                    (session, queryString);
            } catch (NHibernate.OData.ODataException) {
                queryCriteria = session.CreateCriteria<AptifriedClass>();
                throw new System.Web.HttpException(500, "Homie don't play that.");
            }
            var hibernatedCol = queryCriteria.List<AptifriedClass>();



            IList<AptifriedClassDto> classDto = new List<AptifriedClassDto>();
            classDto = Mapper.Map(hibernatedCol, new List<AptifriedClassDto>());
            return classDto;
        }

        public AptifriedClassDto Get(int id) {
            var hibernatedClass = _repo.Get(id);
            AptifriedClassDto classDto = null;
            classDto = Mapper.Map(hibernatedClass, classDto);
            return classDto;
        }
        
        public AptifriedClassDto Put(AptifriedClassDto classDto) {
            if (classDto != null) {
                
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
