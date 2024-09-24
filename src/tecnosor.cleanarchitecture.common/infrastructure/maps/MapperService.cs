using AutoMapper;
using tecnosor.cleanarchitecture.common.domain.maps;
using tecnosor.cleanarchitecture.common.infrastructure.match.maps;

namespace tecnosor.cleanarchitecture.common.infrastructure.maps
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;
        public MapperService() 
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<FilterProfile>());
            _mapper = config.CreateMapper();
        }

        public TSource Map<TSource>(object origin) => _mapper.Map<TSource>(origin);
    }
}
