using BlogService.Models;
using System.Linq.Expressions;

namespace BlogService.Specifications
{
    public abstract class BaseSpeification<T> where T : class
    {
        public Expression<Func<T, bool>>? WhereClause { get; set; }

        public List<Expression<Func<T, object>>> IncludeClause { get; set; }
            = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public Expression<Func<T, object>>? ThenBy { get; private set; }              
        public Expression<Func<T, object>>? ThenByDescending { get; private set; }    

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        public BaseSpeification(Expression<Func<T, bool>>? whereClause)
        {
            WhereClause = whereClause;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression) =>
            IncludeClause.Add(includeExpression);

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
            => OrderByDescending = orderByDescExpression;

        protected void ApplyThenBy(Expression<Func<T, object>> thenByExpression)
            => ThenBy = thenByExpression;

 
        protected void ApplyThenByDescending(Expression<Func<T, object>> thenByDescExpression)
            => ThenByDescending = thenByDescExpression;

        protected void ApplyPaging(int pageSize, int pageNumber)
        {
            Skip = (pageNumber - 1) * pageSize;
            Take = pageSize;
            IsPagingEnabled = true;
        }
    }
}