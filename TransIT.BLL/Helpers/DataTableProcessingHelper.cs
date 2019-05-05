using System;
using System.Linq;
using System.Linq.Expressions;
using TransIT.DAL.Models.ViewModels;

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
        
        public static Expression OrderByTable<TEntity>(
            Expression expression,
            DataTableRequestViewModel dataFilter)
        {
            DataTableRequestViewModel.ColumnType column;
            var elementType = new Type[0]; // new [] { data.ElementType, typeof(string) };
            foreach (var orderType in dataFilter.Order)
            {
                column = dataFilter.Columns[orderType.Column];
                if (column.Orderable)
                    expression = ComposeOrderByExpression<TEntity>(
                        expression,
                        orderType,
                        elementType,
                        column.Data);
            }

            return expression;
        }
        
        public static MethodCallExpression ComposeOrderByExpression<TEntity>(
            Expression baseExpression,
            DataTableRequestViewModel.OrderType orderType,
            Type[] elementType,
            string columnName) =>
            Expression.Call(
                typeof(IQueryable<TEntity>),
                DetermineOrderByMethodDirection(orderType),
                elementType,
                baseExpression,
                CreateExpressionForNestedTypes(typeof(TEntity), columnName)
            );

        public static string DetermineOrderByMethodDirection(DataTableRequestViewModel.OrderType orderType) =>
            orderType.Dir == DataTableRequestViewModel.DataTableAscending
                ? "OrderBy"
                : orderType.Dir == DataTableRequestViewModel.DataTableDescending
                    ? "OrderByDescending"
                    : throw new ArgumentException(
                        $"{nameof(DataTableRequestViewModel)}.Order[].{orderType.Dir} is incorrect");
        
        public static LambdaExpression CreateExpressionForNestedTypes(Type type, string propertyName) 
        {
            var param = Expression.Parameter(type, "x");
            Expression body = param;
            
            foreach (var member in propertyName.Split('.')) 
                body = Expression.PropertyOrField(body, member);
            
            return Expression.Lambda(body, param);
        }
    }
}
