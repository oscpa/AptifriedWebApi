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

        IEnumerable<AptifriedClass> IAptifriedClassRepository.GetDecendants(int classId) {
            var childClasses = session.CreateSQLQuery(
                @"  SELECT c.*
                    FROM vwClasses c
                    WHERE c.ID in (select * from dbo.fnOscpaGetChildClasses(:classId)) ")
                .AddEntity(typeof(AptifriedClass))
                .SetInt32("classId", classId)
                .List<AptifriedClass>();

            return childClasses;     
        }

        IEnumerable<AptifriedClass> IAptifriedClassRepository.Get(ICriteria criteria) {
            return criteria.List<AptifriedClass>();
        }

        AptifriedClass IAptifriedClassRepository.Get(int id) {
            return session.Query<AptifriedClass>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        AptifriedClass IAptifriedClassRepository.Add(AptifriedClass cl) {
            throw new NotImplementedException();
        }

        void IAptifriedClassRepository.Remove(AptifriedClass cl) {
            throw new NotImplementedException();
        }

        AptifriedClass IAptifriedClassRepository.Update(AptifriedClass cl) {
            throw new NotImplementedException();
        }
    }
}