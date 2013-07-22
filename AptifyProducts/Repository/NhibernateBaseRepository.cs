using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using AptifyWebApi.Models.Shared;
using NHibernate;
using NHibernate.Linq;


namespace AptifyWebApi.Repository
{
    public class NHibernateBaseRepository<TC, T> : BaseRepository<TC> where T : class where TC : ISession
    {
        public NHibernateBaseRepository(TC session)
            : base(session)
        {
        }

        public virtual IQueryable<T> GetAll()
        {
            var query = Context.Query<T>().AsQueryable();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = Context.Query<T>().Where(predicate).AsQueryable();
            return query;
        }

        public virtual void Add(T entity)
        {
            Context.Save(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Delete(entity);
        }

        public virtual void Edit(T entity)
        {
            Context.Update(entity);
        }

        public virtual void Save()
        {
            Context.Flush();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
           return FindBy(predicate).Single();
        }

       
    }

  
}