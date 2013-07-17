#region using

using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Repository;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedClassesController : AptifyEnabledApiController
    {
        private readonly IAptifriedClassRepository _repo;

        public AptifriedClassesController(ISession session)
            : base(session)
        {
            _repo = new HibernatedAptifriedClassRepository(session);
        }

        public IEnumerable<AptifriedClassDto> Get()
        {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedClass>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedClass>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedClass>();
            IList<AptifriedClassDto> classDto = new List<AptifriedClassDto>();
            classDto = Mapper.Map(hibernatedCol, new List<AptifriedClassDto>());
            return classDto;
        }

        public AptifriedClassExtendedDto Get(int id)
        {
            var hibernatedClass = session.QueryOver<AptifriedClassExtended>()
                                         .Where(c => c.Id == id)
                                         .SingleOrDefault();

            AptifriedClassExtendedDto classDto = null;
            classDto = Mapper.Map(hibernatedClass, classDto);
            return classDto;
        }

        public AptifriedClassDto Put(AptifriedClassDto classDto)
        {
            if (classDto != null)
            {
            }
            return classDto;
        }

        public IQueryable<AptifriedClassDto> GetSessions(int classId)
        {
            var decendants = _repo.GetDecendants(classId);

            IList<AptifriedClassDto> classDto = new List<AptifriedClassDto>();
            classDto = Mapper.Map(decendants, new List<AptifriedClassDto>());

            return classDto.AsQueryable();
        }
    }
}