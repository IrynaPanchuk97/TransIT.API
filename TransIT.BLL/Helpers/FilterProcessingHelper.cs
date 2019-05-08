using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TransIT.BLL.Helpers
{
    public static class FilterProcessingHelper
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            var parameter = Expression.Parameter(source.ElementType, "p");
            var propertyPath = CapitalizeSentence(orderByProperty);
            var propertyAccess = GetAccessProperty(parameter, propertyPath);
            var property = GetPropertyByPath(
                source.ElementType.GetProperty(propertyPath.First()),
                propertyPath.Skip(1)
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

        private static Expression GetAccessProperty(Expression propertyAccess, IEnumerable<string> propertyPath) =>
            ChangeAndReturn(
                propertyAccess,
                propertyPath,
                (name, prop) => Expression.PropertyOrField(prop, name)
                );

        private static PropertyInfo GetPropertyByPath(PropertyInfo property, IEnumerable<string> propertyPath) =>
            ChangeAndReturn(
                property,
                propertyPath, 
                (name, prop) => prop.PropertyType.GetProperty(name)
                );

        private static T ChangeAndReturn<T>(T property, IEnumerable<string> propertyPath, Func<string, T, T> changer)
        {
            propertyPath.ToList()
                .ForEach(name =>
                    property = changer(name, property)
                    );
            return property;
        }
        
        private static string[] CapitalizeSentence(string str) =>
            str.Split('.')
                .Select(Capitalize)
                .ToArray();
        
        private static string Capitalize(string str) =>
            $"{str.First().ToString().ToUpper()}{str.Substring(1)}";
    }
}
