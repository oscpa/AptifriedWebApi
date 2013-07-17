#region using

using System.Collections.Generic;
using AptifyWebApi.Models;
using NHibernate;

#endregion

namespace AptifyWebApi.Repository
{
    internal interface IAptifriedClassRepository
    {
        IEnumerable<AptifriedClass> GetDecendants(int classId);

        IEnumerable<AptifriedClass> Get(ICriteria criteria);

        AptifriedClass Get(int id);

        AptifriedClass Add(AptifriedClass cl);

        void Remove(AptifriedClass cl);

        AptifriedClass Update(AptifriedClass cl);
    }
}