using tecnosor.cleanarchitecture.common.domain.match;
using Microsoft.EntityFrameworkCore;

namespace tecnosor.cleanarchitecture.common.infrastructure.match;

public class MatchServiceORMEF<TAggregate> : IMatch<TAggregate> where TAggregate : class
{
    private readonly IQueryable<TAggregate> _data;

    public MatchServiceORMEF(IQueryable<TAggregate> data) => _data = data ?? throw new ArgumentNullException(nameof(data));
    
    public IQueryable<TAggregate> MatchQueryable(ISet<Filter<TAggregate>> filterList)
    {
        var query = _data;

        foreach (var filter in filterList)
        {
            var parameter = Expression.Parameter(typeof(TAggregate), "x");
            var member = GetMemberExpression(parameter, filter.Field);
            var constant = Expression.Constant(filter.Value);
            var comparison = GetComparisonExpression(member, filter.Operation, constant);

            var lambda = Expression.Lambda<Func<TAggregate, bool>>(comparison, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

    private static MemberExpression GetMemberExpression(ParameterExpression parameter, string field)
    {
        var parts = field.Split('.');
        Expression expression = parameter;

        foreach (var part in parts) expression = Expression.PropertyOrField(expression, part);

        return (MemberExpression) expression;
    }

    private static Expression GetComparisonExpression(Expression member, Criteria operation, Expression value)
    {
        switch (operation)
        {
            case Criteria.LOWEREQUALS:
                return Expression.LessThanOrEqual(member, value);
            case Criteria.LOWER:
                return Expression.LessThan(member, value);
            case Criteria.HIGHEREQUALS:
                return Expression.GreaterThanOrEqual(member, value);
            case Criteria.HIGHER:
                return Expression.GreaterThan(member, value);
            case Criteria.EQUALS:
                return Expression.Equal(member, value);
            case Criteria.CONTAINS:
                if (member.Type != typeof(string) || value.Type != typeof(string))
                    throw new ArgumentException("CONTAINS operation is only valid for strings.");
                return Expression.Call(member, nameof(string.Contains), null, value);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public List<TAggregate> Match(ISet<Filter<TAggregate>> filterList) =>  MatchQueryable(filterList).ToList();
}
