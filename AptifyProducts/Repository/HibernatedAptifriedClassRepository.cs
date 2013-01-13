using AptifyWebApi.Models;
using NHibernate;
using NHibernate.Linq;
using FluentNHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Repository {
    public class HibernatedAptifriedClassRepository : IAptifriedClassRepository {

        private ISession session;

        public HibernatedAptifriedClassRepository(ISession session) {
            this.session = session;
        }

        public IEnumerable<AptifriedClass> GetAll() {

            var allClasses = session.Query<AptifriedClass>()
                .Where(x => x.StartDate >= DateTime.Now)
                .OrderBy(x => x.StartDate);

            return allClasses;
        }
            

        public AptifriedClass Add(AptifriedClass cl) {
            throw new NotImplementedException();
        }

        public void Remove(AptifriedClass cl) {
            throw new NotImplementedException();
        }

        public AptifriedClass Update(AptifriedClass cl) {
            throw new NotImplementedException();
        }

        public IEnumerable<AptifriedClass> GetDecendants(int classId) {
            var childClasses = session.CreateSQLQuery(
                @"  SELECT c.*
                    FROM vwClasses c
                    WHERE c.ID in (select * from dbo.fnOscpaGetChildClasses(:classId)) ")
                .AddEntity(typeof(AptifriedClass))
                .SetInt32("classId", classId)
                .List<AptifriedClass>();

            return childClasses;               

        }
    }
}