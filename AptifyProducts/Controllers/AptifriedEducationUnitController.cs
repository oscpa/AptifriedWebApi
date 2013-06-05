using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Controllers {
    [Authorize]
    public class AptifriedEducationUnitController : AptifyEnabledApiController {
        public AptifriedEducationUnitController(ISession session) : base(session) { }


        public IList<AptifriedEducationUnitDto> Get() {

            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedEducationUnit>();
            try {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?")) {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = ODataParser.ODataQuery<AptifriedEducationUnit>
                    (session, queryString);

            } catch (NHibernate.OData.ODataException odataException) {
                throw new System.Web.HttpException(500, "Homie don't play that.", odataException);
            }

            var hibernatedCol = queryCriteria.List<AptifriedEducationUnit>();
            IList<AptifriedEducationUnitDto> educationUnits = new List<AptifriedEducationUnitDto>();
            educationUnits = Mapper.Map(hibernatedCol, new List<AptifriedEducationUnitDto>());
            return educationUnits;
        }

        public AptifriedEducationUnitDto Post(AptifriedEducationUnitDto educationUnit) {

            try {
                var educationUnitEntity = AptifyApp.GetEntityObject("Education Unit", -1);
                educationUnitEntity.SetAddValue("PersonID", educationUnit.Person.Id);
                educationUnitEntity.SetAddValue("DateEarned", educationUnit.DateEarned);
                educationUnitEntity.SetAddValue("DateExpires", educationUnit.DateExpires);
                educationUnitEntity.SetAddValue("EducationCategoryID", educationUnit.EducationCategory.Id);
                educationUnitEntity.SetAddValue("Status", educationUnit.Status);
                educationUnitEntity.SetAddValue("EducationUnits", educationUnit.EducationUnits);
                educationUnitEntity.SetAddValue("Source", educationUnit.Source);
                educationUnitEntity.SetAddValue("ExternalSource", educationUnit.ExternalSource);
                educationUnitEntity.SetAddValue("ExternalSourceDescription", educationUnit.ExternalSourceDescription);

                // Don't let people do this. They could grant credit to themselves and verify it.
                //educationUnitEntity.SetAddValue("ExternalSourceVerified", educationUnit.ExternalSourceVerified);
                //educationUnitEntity.SetAddValue("MeetingID", educationUnit.Meeting.Id):
                //educationUnitEntity.SetAddValue("Deactivate", educationUnit.Deactivate);

                string entityError = string.Empty;
                if (!educationUnitEntity.Save(false, ref entityError)) {
                    throw new ApplicationException(string.Format("Could not save new credit. Entity save error was: {0}", entityError));
                } else {
                    educationUnit.Id = System.Convert.ToInt32(educationUnitEntity.RecordID);
                }
            } catch (Exception ex) {
                throw new HttpException(500, "Error saving Education Unit", ex);
            }
            
            // if we've reached here, the dto passed in should now have an "Id" that is the database identifier
            return educationUnit;
        }

    }
}