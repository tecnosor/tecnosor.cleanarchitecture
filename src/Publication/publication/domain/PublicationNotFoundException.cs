using tecnosor.cleanarchitecture.common.domain;

namespace stolenCars.publication.domain;
[Serializable]
public class PublicationNotFoundException : NotFoundException 
{
    public PublicationNotFoundException() : base() { }
    public PublicationNotFoundException(string message) : base(message) { }
    public PublicationNotFoundException(string message, Exception ex) : base(message, ex) { }
}
