#region using

using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto.SessionSelection;
using AptifyWebApi.Models.SessionSelection;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

#endregion

namespace AptifyWebApi.Controllers
{
    [Authorize]
    public class AptifriedMeetingSessionLogSesSelController : AptifriedMeetingSessionLogSesSelBaseController
    {
        public AptifriedMeetingSessionLogSesSelController(ISession session) : base(session)
        {
        }


        /// <summary>
        /// Queries with ODATA over the meeting session log object.
        /// </summary>
        /// <returns></returns>
        public IList<AptifriedMeetingSessionLogDto> Get()
        {
            var queryString = Request.RequestUri.Query;
            ICriteria queryCriteria = session.CreateCriteria<AptifriedMeetingSessionLog>();
            try
            {
                if (!string.IsNullOrEmpty(queryString) && queryString.Contains("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                queryCriteria = session.ODataQuery<AptifriedMeetingSessionLog>(queryString);
            }
            catch (ODataException odataException)
            {
                throw new HttpException(500, "Homie don't play that.", odataException);
            }
            var hibernatedCol = queryCriteria.List<AptifriedMeetingSessionLog>();
            return Mapper.Map(hibernatedCol, new List<AptifriedMeetingSessionLogDto>());
        }

        /// <summary>
        /// Submits a set of sesison changes to the meeting session log object to be processed
        /// by Aptify's back end logic.
        /// </summary>
        /// <param name="sessionChanges"></param>
        /// <returns></returns>
        public AptifriedMeetingSessionLogDto Post(AptifriedMeetingSessionLogDto sessionChanges)
        {
            var sessionChangeEntity = base.AddRegistrationsAndCancellationsToMeetingSessionLog(sessionChanges);
            //var sessionsInConflict = base.FindAnyConflictsInSessionChanges(sessionChangeEntity);

            string errorMessage = string.Empty;
            if (!sessionChangeEntity.Save(false, ref errorMessage))
            {
                throw new HttpException(500,
                                        string.Format(
                                            "An error occurred trying to process session changes. (Error: {0}) ",
                                            errorMessage));
            }

            return sessionChanges;
        }
    }
}