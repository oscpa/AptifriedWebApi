﻿using AptifyWebApi.Dto;
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


        /// <summary>
        ///  Basic getter for odata queries to the education units.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Saves an education unit (with a few caveat) and returns the education unit with a new id.
        /// </summary>
        /// <param name="educationUnit"></param>
        /// <returns></returns>
        public AptifriedEducationUnitDto Post(AptifriedEducationUnitDto educationUnit) {

            try {

                // Allow for updates to existing CPE
                int theRecordId = -1;
                if (educationUnit.Id > 0) 
                    theRecordId = educationUnit.Id;

                var educationUnitEntity = AptifyApp.GetEntityObject("Education Units", theRecordId);

                educationUnitEntity.SetAddValue("PersonID", educationUnit.Person.Id);
                educationUnitEntity.SetAddValue("DateEarned", educationUnit.DateEarned);
                educationUnitEntity.SetAddValue("DateExpires", educationUnit.DateExpires);
                educationUnitEntity.SetAddValue("EducationCategoryID", educationUnit.EducationCategory.Id);
                educationUnitEntity.SetAddValue("Status", educationUnit.Status);
                educationUnitEntity.SetAddValue("EducationUnits", educationUnit.EducationUnits);
                educationUnitEntity.SetAddValue("Source", educationUnit.Source);
                educationUnitEntity.SetAddValue("ExternalSource", educationUnit.ExternalSource);
                educationUnitEntity.SetAddValue("ExternalSourceDescription", educationUnit.ExternalSourceDescription);
                educationUnitEntity.SetAddValue("ExternalCPECity", educationUnit.ExternalCPECity);
                educationUnitEntity.SetAddValue("ExternalCPEInstructor", educationUnit.ExternalCPEInstructor);
                educationUnitEntity.SetAddValue("ExternalCPESponsor", educationUnit.ExternalCPESponsor);

                // Don't let people do this. They could grant credit to themselves and verify it.
				// Response: But Joel, if we don't do it, they'll never be able to add external credits!
				if (educationUnit.Source.Equals("External")) {
					educationUnitEntity.SetAddValue("ExternalSourceVerified", educationUnit.ExternalSourceVerified);
					//educationUnitEntity.SetAddValue("MeetingID", educationUnit.Meeting.Id):
					educationUnitEntity.SetAddValue("Deactivate", educationUnit.Deactivate);
				}

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

		public bool Delete(int educationUnitId) {
			if (educationUnitId < 1) {
				throw new HttpException(500, "No ID given");
			}

			var educationUnitEntity = AptifyApp.GetEntityObject("Education Units", educationUnitId);
			if (educationUnitEntity == null) {
				throw new HttpException(500, "Error loading up Education Units GE");
			}

			AptifriedEducationUnitAttachmentController euaCtrl = new AptifriedEducationUnitAttachmentController(session);
			if (euaCtrl == null) {
				throw new HttpException(500, "Couldn't load up education unit attachment controller");
			}

			var euAttachments = euaCtrl.GetForEducationUnitId(Convert.ToInt64(educationUnitId));

			if (euAttachments != null) {
				try {
					foreach (var euAt in euAttachments) {
						euaCtrl.Delete(euAt.Id);
					}
				} catch (Exception ex) {
					throw new HttpException(500, "Error deleting education unit attachments: " + ex.Message, ex);
				}
			}

			if (!educationUnitEntity.Delete()) {
				throw new HttpException(500, "Error deleting entity: " + educationUnitEntity.LastUserError);
			}

			return true;
		}

        /// <summary>
        /// Returns the trascript formatted credits aggregate education units.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<AptifriedEducationUnitAggregateDto> GetAggregateCredits(int personId, DateTime startDate, DateTime endDate, bool includeExternal) {

            List<AptifriedEducationUnitAggregateDto> resultingData = new List<AptifriedEducationUnitAggregateDto>();

           // string transcriptQuery = "select  Name , Location , Type , Credit , Dates , ProductID " +
                //" from fnOscpaTranscriptGetEducationUnitAggregate(:personId, :startDate, :endDate, :includeExternal, default, default) ";

            string transcriptQuery = "SELECT Name,Location,Type,SUM(Credit),Dates FROM fnOscpaTranscriptGetEducationUnitAggregate(:personId, :startDate, :endDate, :includeExternal, default, default) Group BY Name,Location,Type,Dates";

            var transcriptData = session.CreateSQLQuery(transcriptQuery)
                .SetParameter("personId", personId)
                .SetParameter("startDate", startDate)
                .SetParameter("endDate", endDate)
                .SetParameter("includeExternal", includeExternal)
                .List();

            foreach (object[] row in transcriptData) {
                resultingData.Add(new AptifriedEducationUnitAggregateDto() {
                    Name = Convert.ToString(row[0]),
                    Location = Convert.ToString(row[1]),
                    CreditTypeCode = Convert.ToString(row[2]),
                    Credits = Convert.ToDecimal(row[3]),
                    FormattedDates = Convert.ToString(row[4]),
                    //ProductId = Convert.ToInt32(row[5])
                });
                    
            }

            return resultingData;
        }

        

    }
}