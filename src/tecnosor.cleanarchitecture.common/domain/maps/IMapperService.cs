namespace tecnosor.cleanarchitecture.common.domain.maps;

public interface IMapperService
{
    TSource Map<TSource>(object origin);
}