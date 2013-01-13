using AptifyWebApi.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptifyWebApi.Repository {
    interface IAptifriedClassRepository {


        IEnumerable<AptifriedClass> GetDecendants(int classId);

        IEnumerable<AptifriedClass> GetAll();

        AptifriedClass Add(AptifriedClass cl);

        void Remove(AptifriedClass cl);

        AptifriedClass Update(AptifriedClass cl);
    }
}
