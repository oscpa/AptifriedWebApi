using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate;

namespace AptifyWebApi.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }

    public class BaseRepository<TC>
    {
        protected readonly TC Context;

        protected BaseRepository(TC context)
        {
            Context = context;
        }
    }
}