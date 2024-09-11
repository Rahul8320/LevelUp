using System.Linq.Expressions;

namespace HPlusSport.API.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderByCustom<T>(
        this IQueryable<T> source,
        string propertyName,
        bool ascending)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Property name cannot be null or whitespace.", nameof(propertyName));
        }

        // Get the type of T
        var type = typeof(T);

        // Get the property info
        var property = type.GetProperty(propertyName);
        if (property == null)
        {
            throw new ArgumentException($"Property '{propertyName}' not found on type '{type.Name}'", nameof(propertyName));
        }

        // Create the parameter expression for the lambda
        var parameter = Expression.Parameter(type, "p");

        // Create the property access expression
        var propertyAccess = Expression.Property(parameter, property);

        // Create the lambda expression
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        // Get the appropriate method name
        var methodName = ascending ? "OrderBy" : "OrderByDescending";

        // Create the OrderBy method call
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(type, property.PropertyType);

        // Create the method call expression
        var resultExpression = Expression.Call(
            method,
            source.Expression,
            Expression.Quote(orderByExpression));

        // Return the ordered query
        return source.Provider.CreateQuery<T>(resultExpression);
    }
}
