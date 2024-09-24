using tecnosor.cleanarchitecture.common.domain.match;

namespace stolenCars.publication.domain;

public interface IPublicationRepository : IMatch<Publication>
{
    public Publication GetPublicationById(string id);
    public Task<Publication> GetPublicationByIdAsync(string id);
    public void DeletePublicationById(string id);
    public Task DeletePublicationByIdAsync(string id);
    public Publication UpdatePublication(Publication publication);
    public Task<Publication> UpdatePublicationAsync(Publication publication);
    public Publication CreatePublication(Publication publication);
    public Task<Publication> CreatePublicationAsync(Publication publication);
}
