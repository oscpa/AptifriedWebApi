using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Controllers {
    [System.Web.Http.Authorize]
    public class AptifriedAnswerSheetController : AptifyEnabledApiController {

        public AptifriedAnswerSheetController(ISession session) : base(session) { }

        public IList<AptifriedAnswerSheetDto> Get() {
            // Use the odata query parsing engine to 
            // try to limit hits to the database.
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedAnswerSheet>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedAnswerSheet>
                    (session, queryString);

                //DetachedCriteria forMyselfAsStudent = DetachedCriteria.For(typeof(AptifriedPerson))
                //    .SetProjection(Projections.Property("Id"))
                //    .Add(Expression.Eq("StudentId", AptifyUser.PersonId));

                //queryCriteria.Add(Subqueries.PropertyEq("StudentId", forMyselfAsStudent));


            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedAnswerSheet>();
            IList<AptifriedAnswerSheetDto> answerSheetsDto = new List<AptifriedAnswerSheetDto>();
            answerSheetsDto = Mapper.Map(hibernatedCol, new List<AptifriedAnswerSheetDto>());
            return answerSheetsDto;
        }

        public AptifriedAnswerSheetDto Post(AptifriedAnswerSheetDto answerSheet) {
            //TODO: save to aptify and return saved object
            // We've determined that it's off to the consultans to wire up the process flow that grants credit 
            // when an exam is passed- we really need to just save the sheet off.
            return null;
        }
    }
}