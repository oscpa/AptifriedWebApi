#region using

using System.Web.Http;
using Aptify.Framework.Application;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Aptifried;
using AptifyWebApi.Models.Dto;
using AptifyWebApi.Repository;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    /// <summary>
    /// Handles the abstraction of dependency injection and getting the Aptify Application object
    /// back from the current thread so that we can interact with the back-end.
    /// <remarks>
    /// Abstract class so that we can stub out some methods and use this as a contract as necessary.
    /// May not be necessary.
    /// </remarks>
    /// </summary>
    public abstract class AptifyEnabledApiController : ApiController
    {
        private AptifyApplication _app;

        private AptifriedAuthroizedUserDto _aptifyUser;

        protected internal AptifyEnabledApiController(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Hibernate session that should be available for data retrieval to 
        /// consumers of this class.
        /// </summary>
        internal ISession session { get; set; }

        /// <summary>
        /// Aptify Application object that is available for data updates/entry via consumers of this class.
        /// </summary>
        protected internal AptifyApplication AptifyApp
        {
            get
            {
                if (_app == null)
                {
                    _app = new AptifyApplication(
                        AptifriedAuthorizationFactory.GetUserCredientails());
                }
                return _app;
            }
        }

        /// <summary>
        /// Used to hold the current user filled in by an authorized request. 
        /// <remarks>If the class is not marked as <code>[Authorized]</code> 
        /// we may not be able to find this information.</remarks>
        /// </summary>
        protected internal AptifriedAuthroizedUserDto AptifyUser
        {
            get
            {
                if (_aptifyUser == null && User.Identity.IsAuthenticated)
                {
                    var thisUser = session.QueryOver<AptifriedWebUser>()
                                          .Where(u => u.UserName == User.Identity.Name)
                                          .SingleOrDefault();
                    _aptifyUser = Mapper.Map(thisUser, new AptifriedAuthroizedUserDto());
                }
                return _aptifyUser;
            }
        }
    }
}