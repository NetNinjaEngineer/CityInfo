
using System.Linq.Expressions;

namespace CityInfo.API;

public static class IQueryableExtensions
{
    public static IQueryable<TSource> ApplySort<TSource>(this IQueryable<TSource> source,
        string? sortExcepression)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (string.IsNullOrEmpty(sortExcepression))
            return source;

        var sortParams = sortExcepression.Split(',');

        foreach (var sortParam in sortParams)
        {
            var (propertyName, isDescending) = ExtractParamDetails(sortParam);

            var type = typeof(TSource);

            var parameter = Expression.Parameter(type, "x");

            var lambdaExpression = Expression.Lambda<Func<TSource, object>>(
                Expression.Convert(Expression.Property(parameter, propertyName), typeof(object)), parameter);

            Expression sortExpressionBody;
            if (isDescending)
            {
                sortExpressionBody = Expression.Call(typeof(Queryable), "OrderByDescending", [type, lambdaExpression.ReturnType], source.Expression, lambdaExpression);
            }
            else
            {
                sortExpressionBody = Expression.Call(typeof(Queryable), "OrderBy", [type, lambdaExpression.ReturnType], source.Expression, lambdaExpression);
            }

            source = source.Provider.CreateQuery<TSource>(sortExpressionBody);
        }

        return source;
    }

    private static (string propertyName, bool isDescending) ExtractParamDetails(string sortParam)
    {
        var parts = sortParam.Trim().Split(' ');
        var propertyName = parts[0];
        var isDescending = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);
        return (propertyName, isDescending);
    }

    public static IQueryable<TEntity> ApplyFilter<TEntity, TProperty>(
        this IQueryable<TEntity> entities,
        Expression<Func<TEntity, TProperty>> propertySelector,
        string filterTerm)
    {
        if (!string.IsNullOrWhiteSpace(filterTerm))
        {
            var trimmedFilterTerm = filterTerm.Trim().ToLowerInvariant();
            var propertyExpression = propertySelector.Body as MemberExpression;

            if (propertyExpression == null || propertyExpression.Member.MemberType != System.Reflection.MemberTypes.Property)
            {
                throw new ArgumentException("Invalid property selector");
            }

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propertyExpression.Member.Name);
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var toStringMethod = typeof(object).GetMethod("ToString");

            var toStringCall = Expression.Call(property, toStringMethod);
            var toLowerCall = Expression.Call(toStringCall, toLowerMethod);

            var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) });
            var equalsExpression = Expression.Call(toLowerCall, equalsMethod, Expression.Constant(trimmedFilterTerm), Expression.Constant(StringComparison.CurrentCultureIgnoreCase));

            var predicate = Expression.Lambda<Func<TEntity, bool>>(equalsExpression, parameter);

            return entities.Where(predicate.Compile()).AsQueryable();
        }

        return entities;
    }
}