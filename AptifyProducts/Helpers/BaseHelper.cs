
 using System;
 using System.Linq.Expressions;
 using System.Reflection;
 using FluentNHibernate.Data;
 using Remotion.Linq.Parsing.ExpressionTreeVisitors;

namespace AptifyWebApi.Helpers
{
    public static class BaseHelper
    {
        public static bool IsNull<T>(this T obj)
        {
            return Equals(obj, default(T));
        }

        public static bool IsNotNull<T>(this T obj)
        {
            return !obj.IsNull();
        }


        public static T NullableToNon<T>(this T? obj) where T : struct
        {
            return obj.HasValue ? (T) obj : default(T);
        }


        public static Expression<Func<T, bool>> AndAlsoCombine<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var adaptedExpr2Body = ReplacingExpressionTreeVisitor.Replace(expr2.Parameters[0],expr1.Parameters[0],expr2.Body);
            
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, adaptedExpr2Body),expr1.Parameters);
        }


        public static Expression<Func<T, bool>> OrElseCombine<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var adaptedExpr2Body = ReplacingExpressionTreeVisitor.Replace(expr2.Parameters[0],expr1.Parameters[0],expr2.Body);
        
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, adaptedExpr2Body), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> OrCombine<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var adaptedExpr2Body = ReplacingExpressionTreeVisitor.Replace(expr2.Parameters[0], expr1.Parameters[0], expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, adaptedExpr2Body), expr1.Parameters);
        }

       

        /*
         Person person = new FactoryEntity<Person>()
    .AssociateWithEntity(p => p.Address, address)
    .InstanceEntity; 
         */
    }
}