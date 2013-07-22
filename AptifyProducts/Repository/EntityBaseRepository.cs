using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using AptifyWebApi.Models.Shared;


namespace AptifyWebApi.Repository
{
    public class EntityBaseRepository<TC, T> : BaseRepository<TC> where T : class where TC : AptifyEntityContext
    {
        public EntityBaseRepository(TC session)
            : base(session)
        {
        }

        public virtual IQueryable<T> GetAll()
        {
            var query = Context.Set<T>().AsQueryable();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = Context.Set<T>().Where(predicate).AsQueryable();
            return query;
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            Context.Entry(entity).State = entity != null ? EntityState.Modified : EntityState.Added;
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
           return FindBy(predicate).Single();
        }

       
    }

  
}