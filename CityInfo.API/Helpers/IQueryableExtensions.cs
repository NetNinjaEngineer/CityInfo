
using System.Linq.Expressions;

namespace CityInfo.API;

public static class IQueryableExtensions
{
    public static IQueryable<TSource> ApplySort<TSource>(this IQueryable<TSource> source,
        string? sortExcepression)
    {
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
                sortExpressionBody = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, lambdaExpression.ReturnType }, source.Expression, lambdaExpression);
            }
            else
            {
                sortExpressionBody = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, lambdaExpression.ReturnType }, source.Expression, lambdaExpression);

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
}
