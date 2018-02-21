namespace Refactor.CodeSmells.Filtering.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    //public static class EsxpressionExtension
    //{
    //    internal static Expression<Func<TEntity, bool>> And<TEntity>(this Expression<Func<TEntity, bool>> a, Expression<Func<TEntity, bool>> b)
    //    {
    //        var exp = Expression.AndAlso(a.Body, b.Body);
    //        return Combine<TEntity>(exp);
    //    }
    //    internal static Expression<Func<TEntity, bool>> Or<TEntity>(this Expression<Func<TEntity, bool>> a, Expression<Func<TEntity, bool>> b)
    //    {
    //        var exp = Expression.OrElse(a.Body, b.Body);
    //        return Combine<TEntity>(exp);
    //    }

    //    private static Expression<Func<TEntity, bool>> Combine<TEntity>(BinaryExpression exp)
    //    {
    //        var param = Expression.Parameter(typeof(TEntity));

    //        return Expression.Lambda<Func<TEntity, bool>>(exp, param); ;
    //    }
    //}
    public static class PredicateBuilderExtension
    {

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {

            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[b.Parameters[0]] = p;

            Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {

            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[b.Parameters[0]] = p;

            Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
    }
    internal class SubstExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;
            if (subst.TryGetValue(node, out newValue))
            {
                return newValue;
            }
            return node;
        }
    }
}
