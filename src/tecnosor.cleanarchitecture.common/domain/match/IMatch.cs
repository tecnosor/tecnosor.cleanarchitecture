namespace tecnosor.cleanarchitecture.common.domain.match;

public interface IMatch<TAggregate>
{
    public IQueryable<TAggregate> MatchQueryable(ISet<Filter<TAggregate>> filterList);
    public List<TAggregate> Match(ISet<Filter<TAggregate>> filterList);
}
