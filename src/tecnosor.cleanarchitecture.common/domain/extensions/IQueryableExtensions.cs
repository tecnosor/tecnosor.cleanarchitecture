namespace tecnosor.cleanarchitecture.common.domain.extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> OrderByDinamic<TEntity>(this IQueryable<TEntity> source, IList<(string Property, bool Descending)> orderByProperties)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");

            IOrderedQueryable<TEntity> orderedQuery = null;

            foreach (var (property, descending) in orderByProperties)
            {
                var propertyInfo = type.GetProperty(property);
                var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                var command = descending ? "OrderByDescending" : "OrderBy";
                var method = typeof(Queryable).GetMethods()
                    .First(m => m.Name == command && m.GetParameters().Length == 2)
                    .MakeGenericMethod(type, propertyInfo.PropertyType);

                if (orderedQuery == null)
                {
                    // First property, use OrderBy
                    orderedQuery = (IOrderedQueryable<TEntity>)method.Invoke(null, new object[] { source, orderByExpression });
                }
                else
                {
                    // Subsequent properties, use ThenBy or ThenByDescending
                    var thenByMethod = typeof(Queryable).GetMethods()
                        .First(m => m.Name == (descending ? "ThenByDescending" : "ThenBy") && m.GetParameters().Length == 2)
                        .MakeGenericMethod(type, propertyInfo.PropertyType);

                    orderedQuery = (IOrderedQueryable<TEntity>)thenByMethod.Invoke(null, new object[] { orderedQuery, orderByExpression });
                }
            }

            return orderedQuery ?? source;
        }
    }
}
