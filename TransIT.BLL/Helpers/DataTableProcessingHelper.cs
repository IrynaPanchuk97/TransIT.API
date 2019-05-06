using System.Linq;
using System.Linq.Expressions;

namespace TransIT.BLL.Helpers
{
    public static class DataTableProcessingHelper
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            orderByProperty = MakeFromUpper(orderByProperty);
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = source.ElementType;
            var parameter = Expression.Parameter(type, "p");
            Expression propertyAccess = parameter;
            var propertyPath = orderByProperty
                .Split('.')
                .Select(MakeFromUpper)
                .ToArray();
            var property = type.GetProperty(propertyPath[0]);
            propertyAccess = Expression.PropertyOrField(propertyAccess, MakeFromUpper(propertyPath[0]));
            for (var i = 1; i < propertyPath.Length; ++i)
            {
                propertyAccess = Expression.PropertyOrField(propertyAccess, MakeFromUpper(propertyPath[i]));
                property = property.PropertyType.GetProperty(propertyPath[i]);
            }
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new [] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        private static string MakeFromUpper(string str) =>
            str.First().ToString().ToUpper() + str.Substring(1);
    }
}
