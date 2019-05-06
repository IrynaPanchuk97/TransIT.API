using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TransIT.BLL.Helpers
{
    public static class DataTableProcessingHelper
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            var parameter = Expression.Parameter(source.ElementType, "p");
            var propertyPath = SplitWithUpper(orderByProperty);
            var propertyAccess = GetAccessProperty(parameter, propertyPath);
            var property = GetPropertyByPath(
                propertyPath.Skip(1).ToArray(),
                source.ElementType.GetProperty(propertyPath.First())
                );

            return source.Provider.CreateQuery<TEntity>(
                Expression.Call(typeof(Queryable),
                    desc ? "OrderByDescending" : "OrderBy",
                    new[] { source.ElementType, property.PropertyType },
                    source.Expression,
                    Expression.Quote(
                        Expression.Lambda(propertyAccess, parameter)
                        )
                    )
                );
        }

        private static Expression GetAccessProperty(Expression propertyAccess, string[] propertyPath)
        {
            propertyPath.ToList().ForEach(name => 
                propertyAccess = Expression.PropertyOrField(propertyAccess, name)
                );
            return propertyAccess;
        }

        private static PropertyInfo GetPropertyByPath(string[] propertyPath, PropertyInfo property)
        {
            propertyPath.ToList().ForEach(name =>
                property = property.PropertyType.GetProperty(name)
                );
            return property;
        }
        
        private static string[] SplitWithUpper(string str) =>
            str
                .Split('.')
                .Select(FromUpper)
                .ToArray();
        
        private static string FromUpper(string str) =>
            str
                .First()
                .ToString()
                .ToUpper() 
            + str
                .Substring(1);
    }
}
