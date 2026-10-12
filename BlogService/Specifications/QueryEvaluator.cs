using BlogService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogService.Specifications
{
    public class QueryEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> query, BaseSpeification<T> baseSpecification) where T: class
        {
            var newQuery = query;

           
            if (baseSpecification.WhereClause != null)
                newQuery = newQuery.Where(baseSpecification.WhereClause);

            
            foreach (var include in baseSpecification.IncludeClause)
            {
                newQuery = newQuery.Include(include);
            }

         
            if (baseSpecification.OrderBy != null)
            {
                newQuery = newQuery.OrderBy(baseSpecification.OrderBy);

                if (baseSpecification.ThenBy != null)
                    newQuery = ((IOrderedQueryable<T>)newQuery).ThenBy(baseSpecification.ThenBy);
                else if (baseSpecification.ThenByDescending != null)
                    newQuery = ((IOrderedQueryable<T>)newQuery).ThenByDescending(baseSpecification.ThenByDescending);
            }
            else if (baseSpecification.OrderByDescending != null)
            {
                newQuery = newQuery.OrderByDescending(baseSpecification.OrderByDescending);

                if (baseSpecification.ThenBy != null)
                    newQuery = ((IOrderedQueryable<T>)newQuery).ThenBy(baseSpecification.ThenBy);
                else if (baseSpecification.ThenByDescending != null)
                    newQuery = ((IOrderedQueryable<T>)newQuery).ThenByDescending(baseSpecification.ThenByDescending);
            }

           
            if (baseSpecification.IsPagingEnabled)
            {
                newQuery = newQuery.Skip(baseSpecification.Skip).Take(baseSpecification.Take);
            }

            return newQuery;
        }
    }
}