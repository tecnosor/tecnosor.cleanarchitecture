using tecnosor.cleanarchitecture.common.domain.extensions;
using tecnosor.cleanarchitecture.common.domain.mediator;
using tecnosor.cleanarchitecture.common.domain.match;
using tecnosor.cleanarchitecture.common.domain.pagination;
using stolenCars.publication.domain;

namespace stolenCars.publication.application.cqrs.querys;

public class FindPublication : IRequest<List<Publication>>
{
    public ISet<Filter<Publication>> filters { get; set; }
    public ISet<(string, bool)> orderBy { get; set; }
    public Pagination Pagination { get; set; }
}

public class FindPublicationHandler : IRequestHandler<FindPublication, List<Publication>>
{
    private readonly IPublicationRepository _publicationRepository;

    public FindPublicationHandler(IPublicationRepository publicationRepository) => _publicationRepository = publicationRepository;

    public async Task<List<Publication>> Handle(FindPublication request, CancellationToken cancellationToken)

    {
        var query = _publicationRepository.MatchQueryable(request.filters)
                                          .OrderByDinamic(request.orderBy.ToList())
                                          .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                                          .Take(request.Pagination.PageSize);

        return await Task.Run(() => query.ToList());
    }
}