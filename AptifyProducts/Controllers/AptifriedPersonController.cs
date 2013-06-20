using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using NHibernate.OData;

namespace AptifyWebApi.Controllers
{
	[System.Web.Http.Authorize]
    public class AptifriedPersonController : AptifyEnabledApiController
    {
		public AptifriedPersonController(ISession session) : base(session) { }

		public IEnumerable<AptifriedPersonDto> Get() {
			ICriteria criteria;
			try {
				String query = Request.RequestUri.Query;

				if (!string.IsNullOrEmpty(query) && query.Substring(0, 1) == @"?") {
					query = query.Remove(0, 1);
				}

				criteria = ODataParser.ODataQuery<AptifriedPerson>(session, query);
			} catch (ODataException exception) {
				throw new HttpException(500, "Ain't gonna fly", exception);
			}

			IList<AptifriedPerson> hibernatedDtos = criteria.List<AptifriedPerson>();
			IList<AptifriedPersonDto> personDtos = Mapper.Map(hibernatedDtos, new List<AptifriedPersonDto>());
			return personDtos;
		}

		public AptifriedPersonDto Get(Int32 personId) {
			var foundUser = session.QueryOver<AptifriedPerson>()
				.Where(x => x.Id == personId)
				.SingleOrDefault();

			if (foundUser != null) {
				return Mapper.Map(foundUser, new AptifriedPersonDto());
			}

			// Be explicit in failing
			return null;
		}

		public AptifriedPersonDto Put(AptifriedPersonDto requestPerson) {
			return createPerson(requestPerson);
		}

		private AptifriedPersonDto createPerson(AptifriedPersonDto requestPerson) {
			if (requestPerson == null) {
				throw new HttpException(500, "No person given");
			}

			ISQLQuery query = session.CreateSQLQuery("select p.* from vwPersonsTinyWebServices p where p.Email1 = :email");
			query.AddEntity("p", typeof(AptifriedPerson));
			query.SetString("email", requestPerson.Email);

			IList<AptifriedPerson> listPersons = query.List<AptifriedPerson>();

			if (listPersons.Count > 0) {
				throw new HttpException(500, "Person with that email already exists");
			}

			AptifyGenericEntityBase gePerson = AptifyApp.GetEntityObject("Persons", -1);
			if (gePerson != null) {
				gePerson.SetValue("FirstName", requestPerson.FirstName);
				gePerson.SetValue("LastName", requestPerson.LastName);
				gePerson.SetValue("Email", requestPerson.Email);

				if (!gePerson.Save(false)) {
					throw new HttpException(500, "Error saving new person object: " + gePerson.LastUserError);
				}
			} else {
				throw new HttpException(500, "Error creating new person object");
			}

			return new AptifriedPersonDto() {
				Id = Convert.ToInt32(gePerson.RecordID),
				FirstName = requestPerson.FirstName,
				LastName = requestPerson.LastName,
				Email = requestPerson.Email
			};
		}
    }
}
